using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text.RegularExpressions;

namespace Cracker.Admin.Filters
{
    public class XssProtectionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ps = context.ActionDescriptor.Parameters;
            foreach (var item in ps)
            {
                if (context.ActionArguments[item.Name] != null)
                {
                    if (item.ParameterType.Equals(typeof(string)))
                    {
                        context.ActionArguments[item.Name] = Sanitize(context.ActionArguments[item.Name]!.ToString()!);
                    }
                    else if (item.ParameterType.IsClass)
                    {
                        this.PostModelFieldFilter(item.ParameterType, context.ActionArguments[item.Name]);
                    }
                }
            }
        }

        /// <summary>
        /// 遍历实体的字符串属性
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        private object? PostModelFieldFilter(Type type, object? obj)
        {
            if (obj != null)
            {
                foreach (var item in type.GetProperties())
                {
                    if (item.GetValue(obj) != null)
                    {
                        //当参数是str
                        if (item.PropertyType.Equals(typeof(string)))
                        {
                            var value = item.GetValue(obj)?.ToString();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                item.SetValue(obj, this.Sanitize(value));
                            }
                        }
                        else if (item.PropertyType.IsClass)//当参数是一个实体
                        {
                            item.SetValue(obj, PostModelFieldFilter(item.PropertyType, item.GetValue(obj)));
                        }
                    }
                }
            }
            return obj;
        }

        private string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // 使用正则表达式移除潜在的XSS攻击代码
            // 注意：以下正则表达式只是一个简单的例子，可能不会覆盖所有XSS攻击向量
            // 在生产环境中，建议使用更完善的库，如AntiXSS Library或HtmlSanitizer
            var scriptRegex = new Regex(@"<script.*?>.*?</script.*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var inputRegex = new Regex(@"<input.*?/>", RegexOptions.IgnoreCase);
            var styleRegex = new Regex(@"<style.*?>.*?</style.*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var iframeRegex = new Regex(@"<iframe.*?>.*?</iframe.*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var srcRegex = new Regex(@"src[\s]*=[\s]*['""]?javascript:", RegexOptions.IgnoreCase);

            input = scriptRegex.Replace(input, string.Empty);
            input = inputRegex.Replace(input, string.Empty);
            input = styleRegex.Replace(input, string.Empty);
            input = iframeRegex.Replace(input, string.Empty);
            input = srcRegex.Replace(input, "src=\"\"");

            return input;
        }
    }
}