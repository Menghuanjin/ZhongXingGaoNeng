using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TengDa.Communication.Robot
{
    public class RobotSocket
    {
        //string ss = "<Sensor><RecCmd>hello</RecCmd><RecStation>1</RecStation><RecRow>2</RecRow><RecColumn>3</RecColumn></Sensor>";
        //   SendAddress(st, ss);
        private static byte[] asynchronousBuffer = new byte[1024];
        /// <summary>
        /// 异步接收
        /// </summary>
        /// <param name="result"></param>
        private static void ReceiveCallback(IAsyncResult result)
        {
          
            var socket = result.AsyncState as Socket;
            var length = socket.EndReceive(result);
            //读取出来消息内容
            var message = Encoding.ASCII.GetString(asynchronousBuffer, 0, length);
            //清空数据，重新开始异步接收
            if (!string.IsNullOrEmpty(message))
            {
                if (message.Contains("FNISHA"))//右边机器人收到任务回复
                {
                    // TengDa.Communication.Robot.RobotBLL.leftCode = ss;
                    while (true)
                    {
                        // TengDa.Communication.Robot.RobotBLL.leftAffirm = false;
                        if (RobotConnect(out TengDa.Communication.SocketBLL.smallRobot[1], TengDa.Communication.IPAddress.rightRobot, 54600, 500))
                        {
                            break;
                        }
                        Thread.Sleep(3000);
                    }
                }
                if (message.Contains("FNISHB"))//左边机器人收到任务回复
                {
                    //  TengDa.Communication.Robot.RobotBLL.rightCode = ss;
                    while (true)
                    {
                      
                        // TengDa.Communication.Robot.RobotBLL.leftAffirm = false;
                        if (RobotConnect(out TengDa.Communication.SocketBLL.smallRobot[0], TengDa.Communication.IPAddress.leftRobot, 54600, 500))
                        {
                            break;
                        }
                        Thread.Sleep(3000);
                    }
                }
                if (message.Contains("FNISHC"))//搬运机器人收到任务回复
                {
                    //  TengDa.Communication.Robot.RobotBLL.rightCode = ss;
                    while (true)
                    {
                        // TengDa.Communication.Robot.RobotBLL.leftAffirm = false;
                        if (RobotConnect(out TengDa.Communication.SocketBLL.transportRobot, TengDa.Communication.IPAddress.transportRobot, 54600, 500))
                        {
                            break;
                        }
                        Thread.Sleep(3000);
                    }
                }
                asynchronousBuffer = new byte[asynchronousBuffer.Length];
                socket.BeginReceive(asynchronousBuffer, 0, asynchronousBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }
        }

        //第一次建立连接自动返回GOOD
        //任务发送机器人自动回复FNISH
        //回复FNISH后才可以发送另外一个任务给机器人执行
        /// <summary>
        /// 机器人异步socket通讯连接
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool RobotConnect(out TengDa.Communication.SocketTarNum rbskt, string IP, int port, int outTime)
        {
            bool affirm = false; string ss = string.Empty; byte[] buffer = new byte[1024];
            IPEndPoint ipEndPoint = new IPEndPoint(System.Net.IPAddress.Parse(IP), Convert.ToInt32(port));
            Socket clienSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clienSocket.ReceiveTimeout = outTime;
                clienSocket.Connect((EndPoint)ipEndPoint);
                clienSocket.Receive(buffer);
                ss = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                if (ss.Contains("GOOD"))//已经连接上,可以发送任务命令
                {
                    affirm = true;
                }            
            }
            catch (Exception EX) { }
            finally
            {
                rbskt = new TengDa.Communication.SocketTarNum()
                {
                    clienSocket = clienSocket,
                    conAffirm = affirm,
                    code = ss
                };
            }
            return affirm;
        }
        /// <summary>
        /// 机器人发送数据
        /// </summary>
        /// <param name="st"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string RobotSendAddress(TengDa.Communication.SocketTarNum rbskt, string address)
        {
            string rev = string .Empty;
            if (rbskt.conAffirm)
            {
                byte[] numArray = new byte[1024];
                byte[] bytes = Encoding.ASCII.GetBytes(address);
                try
                {
                    rbskt.clienSocket.Send(bytes); rbskt.conAffirm = false;
                    //    rbskt.clienSocket.Receive(numArray);       
                    rbskt.clienSocket.BeginReceive(asynchronousBuffer, 0, asynchronousBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), rbskt.clienSocket);
                    numArray = asynchronousBuffer;
                    rev = Encoding.ASCII.GetString(numArray, 0, numArray.Length);
              
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return rev;
        }
    }
}
