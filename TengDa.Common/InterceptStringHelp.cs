using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Common
{
    /// <summary>
    /// 字符串处理类
    /// </summary>
 public    class InterceptStringHelp
    {
        /// <summary>
        /// 单个字符分隔用split截取 返回string数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SingleCharacter(string str )
        {
            return  str.Split('.');
        }
        public static string SplitAddress(string str)
        {
            return str;
        }
        /// <summary>
        /// 多个字符分隔用split截取 返回string数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] MultipleCharacters(string str)
        {
            return str.Split(new char[2] { 'j', '_' });
        }
        /// <summary>
        /// 根据字符串或字符串组来截取字符串 返回string数组
        /// </summary>
        /// <returns></returns>
        public static string[] StringGroupInterception(string str)
        {
           return str.Split(new string[] { "Ji", "jB" }, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 提取字符串中的第i个字符开始的长度为j的字符串；
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="start">开始截取</param>
        /// <param name="length">共结束</param>
        /// <returns></returns>
        public static string ExtractString(string str,int start,int length)
        {

            return str.Substring(start , length);
        }
        /// <summary>
        /// 替换字符串中的特定字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="Replace">需要替代的文字</param>
        /// <param name="Value">代替的文字</param>
        /// <returns></returns>
        public static string ReplacemenString(string str, string Replace, string Value)
        {
            return str.Replace(Replace, Value);
        }
        /// <summary>
        /// 删除字符串中的特定字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="Replace">需要替代</param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string DeleteString(string str, string Replace, string Value = "")
        {
            return str.Replace(Replace, Value);
        }
        /// <summary>
        /// 删除指定位置(第i个)的指定长度（length）的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="Value">从开始删除</param>
        /// <param name="length">至删除</param>
        /// <returns></returns>
        public static string DeleteTheSpecifiedLocation(string str,int  Value , int length)
        {
            return str.Remove(Value, length);
        }
        /// <summary>
        /// 删除末尾
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Delectlast(string str)
        {
            return str.Substring(0, str.Length - 1);
        }
    }
}
