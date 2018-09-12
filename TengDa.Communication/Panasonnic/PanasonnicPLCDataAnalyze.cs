using System;
using System.Collections.Generic;

using System.Text;

namespace TengDa.Communication.Panasonnic
{
    public class PanasonnicPLCDataAnalyze
    {
        /// <summary>
        /// 读取处理得到条码
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static string ReadBarcode(string rev)
        {
            if (string.IsNullOrEmpty(rev))
                return "ERROR";
            rev = rev.Replace("\r", "");
            string ss = HexStr(rev);
            string barcode = "";
            for (int i = 0; i < ss.Length / 2; i++)
            {
                string str = ss.Substring(i * 2, 2);
                if (str.ToUpper() == "0D" && i > 0)
                {
                    return barcode;
                }
                barcode += DecimalConversion.HexStringToASCII(str).Replace("\n", "").Replace("\r", "").Replace("\0", "").Replace("", "");
            }
            return barcode.Replace("00000000000000000000000000000000", "ERROR").ToUpper();
        }


        /// <summary>
        /// 解析弹夹条码
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static string ReadClipBarBarcode(string rev)
        {
            if (string.IsNullOrEmpty(rev))
                return "ERROR";
            rev = rev.Replace("\r", "");
            int lenth = 0;
            string ss = HexStr(rev);
            string barcode = "";
            for (int i = 0; i < ss.Length / 2; i++)
            {
                string str = ss.Substring(i * 2, 2);
                if ( i == 0)
                {
                    if (str.ToUpper() == "0D" && i > 0)
                    {
                        return barcode;
                    }
                    lenth = DecimalConversion.HexToTen(str);
                }
                barcode += DecimalConversion.HexStringToASCII(str).Replace("\n", "").Replace("\r", "").Replace("\0", "").Replace("", "").Replace("", "");
            }
            barcode = barcode.Replace("00000000000000000000000000000000", "ERROR").Replace("", "").ToUpper();
            if (!barcode.Contains("ERROR"))
            {
                barcode = barcode.Substring(0, lenth);
            }
            if (string.IsNullOrEmpty(barcode) || barcode.Contains("ERROR"))
            {
                barcode = "ERROR";
            }
            return barcode;
        }

        /// <summary>
        /// 截取寄存器地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HexStr(string str)
        {

            return str.Substring(6, str.Length - 8);
        }

        /// <summary>
        /// 寄存器数据解析
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static List<string> DTDatalearning(string rev)
        {
            List<string> Data = new List<string>();

            if (string.IsNullOrEmpty(rev))
            {
                return Data;
            }
            rev = rev.Replace("\r", "");
            string ss = HexStr(rev);
            string highOrder = "";
            string lowOrder = "";
            for (int i = 0; i < ss.Length / 4; i++)
            {
                //if (i == 0)
                //{
                //    highOrder = ss.Substring(0, 2);//高位数据
                //    lowOrder = ss.Substring( 2, 2);//低位数据
                //}
                //else
                //{
                //    highOrder = ss.Substring((i * 4), 2);//高位数据  09100910
                //    lowOrder = ss.Substring((i * 4) + 2, 2);//低位数据
                //}
                highOrder = ss.Substring((i * 4), 2);//高位数据  09100910
                lowOrder = ss.Substring((i * 4) + 2, 2);//低位数据


                Data.Add(DecimalConversion.HexToTen(lowOrder + highOrder).ToString());

            }
            return Data;
        }

        /// <summary>
        /// 寄存器数据解析到字典
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static Dictionary<int, string> DTDatalearningToDictionary(string rev)
        {
            Dictionary<int, string> data = new Dictionary<int, string>();

            if (string.IsNullOrEmpty(rev))
            {
                return data;
            }
            rev = rev.Replace("\r", "");
            string ss = HexStr(rev);
            string highOrder = "";
            string lowOrder = "";
            for (int i = 0; i < ss.Length / 4; i++)
            {              
                highOrder = ss.Substring((i * 4), 2);//高位数据  09100910
                lowOrder = ss.Substring((i * 4) + 2, 2);//低位数据
                data.Add(i,DecimalConversion.HexToTen(lowOrder + highOrder).ToString());

            }
            return data;
        }

        /// <summary>
        /// 触点数据解析
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static int RDatalearning(string rev)
        {
            if (!string.IsNullOrEmpty(rev))
                return Convert.ToInt32(rev.Substring(6, 1));
            else
                return -1;
        }

        /// <summary>
        /// 解析一个寄存器数据
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static int DTDatalearningOne(string rev)
        {
            if (string.IsNullOrEmpty(rev))
            {
                return -1;
            }
            rev = rev.Replace("\r", "");
            string ss = HexStr(rev);
            //string highOrder = "";
            //string lowOrder = "";
            //highOrder = ss.Substring((0), 2);//高位数据  09100910
            //lowOrder = ss.Substring((0) + 2, 2);//低位数据
            return DecimalConversion.HexToTen(ss.Substring((0) + 2, 2) + ss.Substring((0), 2));
        }

        public static List<string> RuleDatalearning(string data)
        {
            List<string> list = new List<string>();
            for (int i = data.Length - 1; i >= 0; i--)
            {
                string s = data[i].ToString();
                if (s == "1")
                {
                    list.Add(DecimalConversion.Ten2Hex((data.Length-i-1).ToString()));
                }              
            }
            return list;
        }
    }
}
