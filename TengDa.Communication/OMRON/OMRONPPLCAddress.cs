using Omron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication
{
   public class OMRONPLCAddress
    {
      
        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <param name="Ip">连接IP</param>
        /// <param name="port">连接的端口</param>
        /// <returns></returns>
        public static OmronPLC_tcp ConnectPLC(string Ip, uint port)
        {
            OmronPLC_tcp ConPLC = new Omron.OmronPLC_tcp(Omron.TransportType.Tcp, Ip, port);
            try
            {
                //lock (Locker1)
                //{
                    ConPLC.Connect();
              //  }
            
            }
            catch { return null; }
            return ConPLC.LastError != null || !ConPLC.Connected ? ConPLC : ConPLC;
        }
        /// <summary>
        /// 读单个寄存器
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address">地址</param>
        /// <returns></returns>
        public static string ReadDM(OmronPLC_tcp omronPLC, string Address)
        {
            short value = 0;
            lock (TengDa.Communication.PCLock.Locker)
            {
                omronPLC.ReadDM(Convert.ToUInt16(Address), ref value);
            }
            return value.ToString();
        }
        /// <summary>
        /// 读单个位
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address">地址</param>
        /// <returns></returns>
        public static bool ReadBit(OmronPLC_tcp omronPLC, string Address)
        {
            bool value;
            lock (TengDa.Communication.PCLock.Locker)
            {
                omronPLC.FinsCommand.BoolRead("2." + Address, out value);
            }
            return value;
        }
        /// <summary>
        /// 写单个位
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address">地址</param>
        /// <param name="value">值</param>
        public static bool WriteBit(OmronPLC_tcp omronPLC, string Address, bool value)
        {
            lock (TengDa.Communication.PCLock.Locker)
            {
                return omronPLC.WriteBit("2." + Address, value);
            }
        }
        /// <summary>
        /// 写单个寄存器
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address">地址</param>
        /// <param name="value">值</param>
        public static bool WriteDM(OmronPLC_tcp omronPLC , int Address, int value)
        {
            lock (TengDa.Communication.PCLock.Locker)
            {
                return omronPLC.WriteDM(Convert.ToUInt16(Address), Convert.ToUInt16(value));
            }
           
        }

        /// <summary>
        /// 读多个寄存器
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="Address">地址</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static List<string> ReadMulti(OmronPLC_tcp omronPLC, int Address, int value)
        {
            List<string> List = new List<string> { };
            ushort[] us = new ushort[value];
            lock (TengDa.Communication.PCLock.Locker)
            {
                omronPLC.ReadDMs(Convert.ToUInt16(Address), ref us, Convert.ToUInt16(value));
            }
            foreach (var item in us)
            {
                List.Add(item.ToString());
            }
            return List;
        }


        /// <summary>
        /// 写多个寄存器(list第0个就是开始地址的第0个  list[0]的值=7000   list[1]的值=7001)
        /// </summary>
        /// <param name="omronPLC"></param>
        /// <param name="list"></param>
        /// <param name="Address"></param>
        public static bool WriteMulti(OmronPLC_tcp omronPLC, List<string> list, int Address)
        {
            byte[] bytes1 = new byte[2];
            byte[] bytes2 = new byte[2];
            byte[] bytes3 = new byte[list.Count * 2];
            for (int i = 0; i < list.Count; i++)
            {
                string str = list[i];
                bytes2 = BitConverter.GetBytes(int.Parse(list[i]));
                bytes1[0] = bytes2[1];
                bytes1[1] = bytes2[0];
                bytes3[i * 2] = bytes1[0];
                bytes3[i * 2 + 1] = bytes1[1];
            }
            lock (TengDa.Communication.PCLock.Locker)
            {
                return omronPLC.finsMemoryAreadWrite(MemoryArea.DM, Convert.ToUInt16(Address), Convert.ToUInt16(list.Count), bytes3);
            }
        }
    }
}
