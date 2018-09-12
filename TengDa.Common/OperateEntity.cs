using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Common
{
  public  class OperateEntity
    {
        /// <summary>
        /// 利用反射根据对象和属性名取对应属性的值
        /// </summary>
        /// <param name="feildname"></param>
        /// <param name="obEntity"></param>
        /// <returns></returns>
        public static string GetValueByPropertyName(string feildname, Object obEntity)
        {
            string PropertyVaule = string.Empty;
            Type tpEntity = obEntity.GetType();
            System.Reflection.PropertyInfo[] pis = tpEntity.GetProperties();
            var a = pis.FirstOrDefault(m => m.Name == feildname);
            if (a != null)
            {
                object obj = a.GetValue(obEntity, null);
                if (obj != null)
                {
                    PropertyVaule = obj.ToString();
                }

            }

            return PropertyVaule;
        }
        /// <summary>
        /// 利用反射根据对象和属性名为对应的属性赋值
        /// </summary>
        /// <param name="feildname"></param>
        /// <param name="obEntity"></param>
        /// <returns></returns>
        public static void SetValueByPropertyName(string feildname, Object obEntity, string Value)
        {
            Type tpEntity = obEntity.GetType();
            System.Reflection.PropertyInfo[] pis = tpEntity.GetProperties();
            var a = pis.FirstOrDefault(m => m.Name == feildname);
            if (a != null)
            {
                a.SetValue(obEntity, Convert.ChangeType(Value, a.PropertyType), null);
                return;
            }
        }
    }
}
