using Omron;
using System;
using System.Collections.Generic;
using System.Text;


namespace TengDa.Communication
{
   public  class OMRONPAnalyzeDMHelp
    {
        /// <summary>
        /// 读多个地址并解析
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> ReadMultiToErrorInfo(OmronPLC_tcp omronPLC, int Address, int value)
        {
            List<string> List = new List<string> { };
            List<string> Binary = new List<string> { };
            string lowOrder = "";
            ushort[] us = new ushort[value];
            lock (TengDa.Communication.PCLock.Locker)
            {
                omronPLC.ReadDMs(Convert.ToUInt16(Address), ref us, Convert.ToUInt16(value));
            }
            foreach (var item in us)// 将数值转2进制
            {
                Binary.Add(Convert.ToString(item, 2).PadLeft(16, '0'));
            }
            for (int i = 0; i < Binary.Count; i++)
            {
                if (Binary[i].Contains("1"))//字节里有报警位
                {
                    //0000 0001 0000 1000
                    lowOrder = HeightToLow(Binary[i]);//二进制反转
                    //0001 0000 1000 0000
                    int ii = lowOrder.IndexOf("1");
                    //一个字的排序方式 高位 0000 0001 0000 0110 低位
                    while (ii >= 0 && ii < lowOrder.Length)
                    {
                        List.Add("D" + (Address + i).ToString() + Convert.ToString(ii).PadLeft(2, '0'));
                        ii = lowOrder.IndexOf("1", ii + 1);
                    }
                }
            }
            return List;
        }
        /// <summary>
        /// 二进制反转
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private  static string HeightToLow(string str)
        {
            string lowOrder = "";
            char[] a = str.ToCharArray();
            Array.Reverse(a);
            foreach (char c in a)
            {
                lowOrder += c;
            }
            return lowOrder;
        }
        /// <summary>
        /// 读多个寄存器转换温度
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address">地址</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static List<string> ReadMultiTem(OmronPLC_tcp omronPLC, int Address, int value)
        {
            List<string> List = new List<string> { };
            ushort[] us = new ushort[value];
            lock (TengDa.Communication.PCLock.Locker)
            {
                omronPLC.ReadDMs(Convert.ToUInt16(Address), ref us, Convert.ToUInt16(value));
            }
            foreach (var item in us)
            {
                List.Add(((double)item/10).ToString("0.0"));
            }
            return List;
        }
        /// <summary>
        ///  读多个寄存器转换状态
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address">地址</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static List<string> ReadMultiToEnum(OmronPLC_tcp omronPLC, int Address, int value)
        {
            List<string> List = new List<string> { };
            ushort[] us = new ushort[value];
            lock (TengDa.Communication.PCLock.Locker)
            {
                omronPLC.ReadDMs(Convert.ToUInt16(Address), ref us, Convert.ToUInt16(value));
            }
            foreach (var item in us)
            {
                TengDa.Common.SystemEnum.MacChuStatus ss = (TengDa.Common.SystemEnum.MacChuStatus)Convert.ToInt16(item);
                string description = TengDa.Common.EnumHelper.GetDescription(ss);
                List.Add(description);
            }
            return List;
        }
    }
}
