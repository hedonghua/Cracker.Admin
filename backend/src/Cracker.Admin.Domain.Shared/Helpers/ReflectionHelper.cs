using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cracker.Admin.Helpers
{
    public class ReflectionHelper
    {
        /// <summary>
        /// 获取项目程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetMyAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName != null && x.FullName.Contains("Cracker.Admin")).ToArray();
        }

        /// <summary>
        /// 同属性名赋值（基于反射）
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        /// <param name="ignoreProps">忽略属性</param>
        public static void AssignTo<TSource, TTarget>(TSource source, TTarget target, params string[] ignoreProps)
        {
            if (source == null || target == null) return;
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            ignoreProps ??= [];
            var map = new Dictionary<string, object?>();
            foreach (var sourceProp in sourceType.GetProperties())
            {
                map.Add(sourceProp.Name, sourceProp.GetValue(source));
            }
            foreach (var targetProp in targetType.GetProperties())
            {
                string propName = targetProp.Name;
                if (ignoreProps.Contains(propName)) continue;
                if (map.TryGetValue(propName, out object? value))
                {
                    //Convert.ChangeType(value, targetProp.PropertyType)
                    targetProp.SetValue(target, value);
                }
            }
        }
    }
}