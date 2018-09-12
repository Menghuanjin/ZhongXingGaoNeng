using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace TengDa.Common
{
   public  class PingHelp
    {
        public static bool TestPing(string ip)
        {
            Ping pingSender = new Ping();

            PingReply reply = pingSender.Send(ip, 120);

            return reply.Status == IPStatus.Success ? true : false;
        }
    }
}
