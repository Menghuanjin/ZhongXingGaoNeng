using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TengDa.Common
{
    public class ObjectReflection
    {
        public static PropertyInfo[] GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        /// <summary>
        /// 实体属性反射
        /// </summary>
        /// <typeparam name="S">赋值对象</typeparam>
        /// <typeparam name="T">被赋值对象</typeparam>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public static void AutoMapping<S, T>(S s, T t)
        {
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            Type target = t.GetType();

            foreach (var pp in pps)
            {
                PropertyInfo targetPP = target.GetProperty(pp.Name);
                object value = pp.GetValue(s, null);

                if (targetPP != null && value != null)
                {
                    targetPP.SetValue(t, value, null);
                }
            }
        }
    }
}
