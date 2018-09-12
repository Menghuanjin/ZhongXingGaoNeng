using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TengDa.Communication.Panasonnic
{
    public class PanasonnicSocket
    {
        public static bool IsCoon { get; set; }

        /// <summary>
        /// socket通讯连接
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static Socket PanasonnicConn(string IP, int port, int outTime)
        {
            IPEndPoint ipEndPoint = new IPEndPoint(System.Net.IPAddress.Parse(IP), Convert.ToInt32(port));
            Socket clienSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clienSocket.ReceiveTimeout = outTime;
                clienSocket.Connect((EndPoint)ipEndPoint);
                IsCoon = true;
            }
            catch {}
            return clienSocket;
        }
        public static string PanasonnicSendAddress(Socket st, string address)
        {
            string rev = string.Empty;
            lock (TengDa.Communication.PCLock.Locker)
            {
                try
                {
                    if (!st.Connected)
                    {
                        IsCoon = false;
                    }
                    byte[] bytes = Encoding.ASCII.GetBytes(address + "\r");
                    byte[] numArray = new byte[1024];

                    st.Send(bytes);
                    st.Receive(numArray);
                    rev = Encoding.ASCII.GetString(numArray).Replace("\0", "");
                    if (rev.Contains("!"))//未读到了正确数据
                    {
                        rev = "";
                    }
                }
                catch (Exception ex)
                {
                    rev = "";
                }
            }
            return rev;
        }
    }
}
