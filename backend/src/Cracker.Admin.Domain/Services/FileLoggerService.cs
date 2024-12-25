using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cracker.Admin.Core;

namespace Cracker.Admin.Services
{
    public class FileLoggerService : IFileLogger
    {
        private readonly Queue<WriteFileLogRequest> _queues = new();
        private readonly ConcurrentDictionary<WriteFileLogRequest, int> _keyValuePairs = new();

        public static readonly IFileLogger Instance = LazyValue.Value;

        private static Lazy<IFileLogger> LazyValue => new(() =>
        {
            return new FileLoggerService();
        });

        public void Write(string content, string? topic = default, Exception? exception = default, params object[] args)
        {
            var req = new WriteFileLogRequest
            {
                Content = content,
                Topic = topic,
                Exception = exception,
                Args = args
            };
            _queues.Enqueue(req);

            Task.Run(Consume);
        }

        public void Consume()
        {
            int i = 0;
            while (i < 10 && _queues.Count > 0)
            {
                if (!_queues.TryDequeue(out var req) || req == null) continue;
                try
                {
                    var topic = req.Topic;
                    var exception = req.Exception;
                    var args = req.Args;
                    var content = req.Content;

                    var time = DateTime.Now;
                    var date = time.ToString("yyyy-MM-dd");
                    var todayDir = Path.Combine(AppManager.WebRoot!, "logs", date);
                    if (!Directory.Exists(todayDir)) Directory.CreateDirectory(todayDir);
                    string fileName = date.Replace("-", "");
                    if (!string.IsNullOrWhiteSpace(topic))
                    {
                        fileName = topic;
                    }
                    else if (exception != null)
                    {
                        fileName = "exception";
                    }
                    fileName += ".log";
                    fileName = Path.Combine(todayDir, fileName);

                    var sb = new StringBuilder(2048);
                    if (args != null && args.Length > 0)
                    {
                        content = string.Format(content, args);
                    }
                    sb.AppendFormat("[{0}]: ", time.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                    sb.Append('\t');
                    sb.AppendLine(content);
                    if (exception != null)
                    {
                        sb.AppendLine("<!-- exception -->");
                        sb.AppendLine(exception.Message);
                        sb.AppendLine("<!-- exception track -->");
                        sb.AppendLine(exception.StackTrace);
                    }

                    lock (this)
                    {
                        File.AppendAllText(fileName, sb.ToString());
                    }
                }
                catch (Exception)
                {
                    if (_keyValuePairs.TryGetValue(req, out int value))
                    {
                        //每个重试3次
                        if (value > 3) continue;
                        _keyValuePairs[req] += 1;
                    }
                    else
                    {
                        _keyValuePairs.TryAdd(req, 1);
                    }
                    _queues.Enqueue(req);
                }
                finally
                {
                    i++;
                }
            }
        }
    }

    internal class WriteFileLogRequest
    {
        [NotNull]
        public string? Content { get; set; }

        public string? Topic { get; set; }

        public Exception? Exception { get; set; }

        public object[]? Args { get; set; }
    }
}