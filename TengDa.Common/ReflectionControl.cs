using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TengDa.Common
{
   public class ReflectionControl
    {

        public  T GetControlObject<T>(string controlName)
        {
            try
            {
                Type type = this.GetType();
                FieldInfo fieldInfo = type.GetField(controlName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                if (fieldInfo != null)
                {
                    T obj = (T)fieldInfo.GetValue(this);
                    return obj;
                }
                else
                {
                    return default(T);
                }

            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
