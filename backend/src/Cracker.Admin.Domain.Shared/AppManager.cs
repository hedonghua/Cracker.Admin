using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin
{
    public static class AppManager
    {
        /// <summary>
        /// 是否开发环境
        /// </summary>
        public static bool IsDevelopment { get; private set; }

        /// <summary>
        /// wwwroot磁盘绝对地址
        /// </summary>
        public static string? WebRoot { get; private set; }

        /// <summary>
        /// appsettings.json配置
        /// </summary>
        [NotNull]
        public static IConfiguration? Configuration { get; private set; }

        [NotNull]
        public static IServiceProvider? ServiceProvider { get; private set; }

        public static void BeforeSet(IConfiguration configuration, string? webRoot)
        {
            Configuration = configuration;
            WebRoot = webRoot;
        }

        public static void AfterSet(IServiceProvider serviceProvider, bool isDev)
        {
            ServiceProvider = serviceProvider;
            IsDevelopment = isDev;
        }
    }
}