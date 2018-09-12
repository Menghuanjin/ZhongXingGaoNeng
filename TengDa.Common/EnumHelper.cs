using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TengDa.Common
{
    public static class EnumHelper
    {
        /// <summary>
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstend">当枚举没有定义DescriptionAttribute,是否用枚举名代替，默认使用</param>
        /// <returns>枚举的Description</returns>
        //public static string GetDescription(this Enum value, bool nameInstend = true)
        //{
        //    Type type = value.GetType();
        //    string name = Enum.GetName(type, value);
        //    if (name == null)
        //    {
        //        return null;
        //    }
        //    FieldInfo field = type.GetField(name);
        //    DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        //    if (attribute == null && nameInstend == true)
        //    {
        //        return name;
        //    }
        //    return attribute == null ? null : attribute.Description;
        //}

        //public static void GetDescriptionByValue(enum enumType, int EnumValue)
        //{

        //    string name = typeof(enumType).GetEnumName(EnumValue);
        //    FieldInfo fieldinfo = typeof(enumType).GetField(name);
        //    Object obj = fieldinfo.GetCustomAttribute(typeof(DescriptionAttribute), false);
        //    DescriptionAttribute ds = (DescriptionAttribute)obj;

        //    Console.WriteLine(ds.Description);
        //    Console.ReadLine();

        //}
        public static string FetchDescription(this Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch
            {
                return "";
            }
           
        }
        public static string GetDescription(this Enum enumName)
        {
            string str = string.Empty;
            DescriptionAttribute[] descriptAttr = enumName.GetType().GetField(enumName.ToString()).GetDescriptAttr();
            return descriptAttr == null || descriptAttr.Length == 0 ? enumName.ToString() : descriptAttr[0].Description;
        }
        public static ArrayList ToArrayList(this Enum en)
        {
            ArrayList arrayList = new ArrayList();
            foreach (Enum @enum in Enum.GetValues(en.GetType()))
                arrayList.Add(new KeyValuePair<Enum, string>(@enum, @enum.GetDescription()));
            return arrayList;
        }

        public static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            if (fieldInfo != null)
                return (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return null;
        }
    }
}
