using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication.Panasonnic
{
  public  class PanasonnicPLCAddress
    {
        /// <summary>
        /// 读单个寄存器地址
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static int PanasonnicReadDT(Socket sk, int address)
        {
            return TengDa.Communication.Panasonnic.PanasonnicPLCDataAnalyze.DTDatalearningOne(TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicSendAddress(sk, TengDa.Communication.Panasonnic.PanasonnicSplitJoint.DTAddressReadConvert(address)));
        }

        /// <summary>
        /// 写单个寄存器地址
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        public static void PanasonnicWriteDT(Socket sk,int address,int value)
        {
            TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicSendAddress(sk, TengDa.Communication.Panasonnic.PanasonnicSplitJoint.DTAddressWriteConvert(address, value));
        }
        /// <summary>
        /// 读单个位
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int PanasonnicReadBit(Socket sk,string  address)
        {
            return TengDa.Communication.Panasonnic.PanasonnicPLCDataAnalyze.RDatalearning(TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicSendAddress(sk,TengDa.Communication.Panasonnic.PanasonnicSplitJoint.RAddressReadConvert(address)));
        }
        /// <summary>
        /// 写单个位
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        public static void PanasonnicWriteBit(Socket sk, string address, int value)
        {
            TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicSendAddress(sk, TengDa.Communication.Panasonnic.PanasonnicSplitJoint.RAddressWrite(address, value));
        }
        /// <summary>
        /// 读多个寄存器
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<string> PanasonnicReadMulti(Socket sk, int begin,int end)
        {
            return TengDa.Communication.Panasonnic.PanasonnicPLCDataAnalyze.DTDatalearning(TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicSendAddress(sk, TengDa.Communication.Panasonnic.PanasonnicSplitJoint.DTAddressReadConvert(begin, end)));
        }
        /// <summary>
        /// 写多个地址
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="address"></param>
        /// <param name="list"></param>
        public static void PanasonnicWriteMulti(Socket sk, int address, List<string> list)
        {

            TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicSendAddress(sk, TengDa.Communication.Panasonnic.PanasonnicSplitJoint.DTAddressWriteConvert(address, list));
        }
    }
}
