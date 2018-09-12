using System;
using System.Collections.Generic;
using System.Text;

namespace TengDa.Communication.Panasonnic
{
    public class PanasonnicSplitJoint
    {
        /// <summary>
        /// 读取多寄存器地址格式
        /// </summary>
        /// <param name="beginAddress">开始地址</param>
        /// <param name="endAddress">结束地址</param>
        /// <returns></returns>
        public static string DTAddressReadConvert(int beginAddress, int endAddress)
        {
            string address = "%01#RDD";// "%01#RDD0030100309**";
            address = address + beginAddress.ToString().PadLeft(5, '0') + endAddress.ToString().PadLeft(5, '0')+"**";
            return address;
        }

        /// <summary>
        /// 读取单个寄存器地址格式
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <returns></returns>
        public static string DTAddressReadConvert(int beginAddress)
        {
            string address = "%01#RDD";// "%01#RDD0030100309**";
            address = address + beginAddress.ToString().PadLeft(5, '0') + beginAddress.ToString().PadLeft(5, '0') + "**";
            return address;
        }


        /// <summary>
        /// 截取寄存器返回值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DTValues(string str)
        {
            return str.Substring(6, str.Length - 8);
        }


        /// <summary>
        /// 解析寄存器读取的值
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static List<string> ReadDTValue(string rev)
        {
            List<string> list = new List<string>();
            //地址 %01#RDD0030100309**
            rev = PanasonnicSplitJoint.DTValues(rev);
            for (int i = 0; i < rev.Length / 2; i++)
            {
                string str = rev.Substring(i * 2, 2);
                list.Add(DecimalConversion.HexStringToASCII(str));
            }
            return list;
        }


        /// <summary>
        /// 解析多个寄存器解析出来拼接成字符串   例如条码解析
        /// </summary>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static string ReadDTValueToStr(string rev)
        {
            //地址 %01#RDD0030100309**
            string ss = PanasonnicSplitJoint.DTValues(rev);
            string barcode = "";
            for (int i = 0; i < ss.Length / 2; i++)
            {
                string str = ss.Substring(i * 2, 2);
                barcode += DecimalConversion.HexStringToASCII(str);
            }
            return barcode;
        }


        /// <summary>
        /// 读取单个触点数据转换
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string RAddressReadConvert(string beginAddress)
        {
            return "%01#RCSR" + beginAddress.ToString().PadLeft(4, '0') + "**"; 
        }

        /// <summary>
        /// 读取多个触点数据转换
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string RAddressReadConvert(string beginAddress, string Length)
        {
            return "%01#RCPR" + beginAddress.ToString().PadLeft(4, '0') + "**"; 
        }

        /// <summary>
        /// 触点写值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RAddressWrite(string address,int value)
        {
            return "%01#WCSR" + address.ToString().PadLeft(4, '0') + value.ToString() + "**"; 
        }



        /// <summary>
        /// 写入多个寄存器的值
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <param name="endAddress"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string DTAddressWriteConvert(int beginAddress,List<string> values)
        {
            string address = "%01#WDD";// "%01#RDD0030100309**";
            //string writeValue = DecimalConversion.Ten2Hex(values.ToString()).PadLeft(4, '0');
            address = address + beginAddress.ToString().PadLeft(5, '0') + (beginAddress+values.Count).ToString().PadLeft(5, '0') ;
            for (int i = 0; i < values.Count; i++)
            {
                string value = DecimalConversion.Ten2Hex(values[i]).PadLeft(4, '0');
                address += value.Substring(2, 2) + value.Substring(0, 2);
            }
            return address + "**";
        }
        /// <summary>
        /// 写入单个寄存器的值
        /// </summary>
        /// <param name="beginAddress">地址</param>
        /// <param name="values">值</param>
        /// <returns></returns>
        public static string DTAddressWriteConvert(int beginAddress,int values)
        {
            string address = "%01#WDD";
            string writeValue = DecimalConversion.Ten2Hex(values.ToString()).PadLeft(4, '0');
            address = address + beginAddress.ToString().PadLeft(5, '0') + beginAddress.ToString().PadLeft(5, '0');
            address += writeValue.Substring(2, 2) + writeValue.Substring(0, 2);
            return address + "**";
        }

        /// <summary>
        ///  按字单位读取触点状态
        /// </summary>
        /// <param name="beginaddress"></param>
        /// <param name="endAddress"></param>
        /// <returns></returns>
        public static string RCCAddressReadConvert(int beginaddress, int endAddress)
        {
            return "%01#RCCR" + beginaddress.ToString().PadLeft(4, '0') + endAddress.ToString().PadLeft(4, '0') + "**";
        }

    }
}
