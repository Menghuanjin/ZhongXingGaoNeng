using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication
{
    public class SocketBLL
    {
        /// <summary>
        /// baking炉子
        /// </summary>
        public static Omron.OmronPLC_tcp[] bakingContent = new Omron.OmronPLC_tcp[9];
        /// <summary>
        /// 上料
        /// </summary>
        public static Socket[] matContent = new Socket[2];
        /// <summary>
        /// 上料机器人
        /// </summary>
        public static SocketTarNum[] smallRobot = new SocketTarNum[2];

        /// <summary>
        /// 搬运机器人
        /// </summary>
        public static SocketTarNum transportRobot;
    }
    public class SocketTarNum
    {
        public Socket clienSocket { get; set; }
        /// <summary>
        /// 是否已经连接上
        /// </summary>
        public bool conAffirm { get; set; }
        /// <summary>
        /// 返回代码
        /// </summary>
        public string code { get; set; }

    }
}
