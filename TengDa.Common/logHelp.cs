using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TengDa.Common
{
    public class logHelp
    {
        private static StreamWriter streamWriter; //写文件 


        /// <summary>
        ///本地log信息
        /// </summary>
        public static string LocalLog { set; get; }

        public static string  TransportLog { get; set; }

        /// <summary>
        /// 搬运机器人搬运日志
        /// </summary>
        /// <param name="meg"></param>
        public static void WriteTransportLog(string meg)
        {
            TransportLog =  string.Format(" {0} ==>{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), meg) + TransportLog;
            TengDa.Common.logHelp.WriteLog(string.Format("{0} ==>{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), meg),1) ;
        }
        public static void WriteLocalLog(string logInfo)
        {
            LocalLog = string.Format(" {0} ==>{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), logInfo) + LocalLog;
        }
        public static void WriteLog(string message , int num)
        {
            try
            {
              
                string directPath = string.Empty;
                if (num == 1)
                {
                    directPath = AppDomain.CurrentDomain.BaseDirectory ;
                    if (!Directory.Exists(directPath))   //判断文件夹是否存在，如果不存在则创建
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"TransportLog\{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));    //获得文件夹路径
                }
                else if (num == 2)
                {
                    directPath = AppDomain.CurrentDomain.BaseDirectory ;
                 
                    if (!Directory.Exists(directPath))   //判断文件夹是否存在，如果不存在则创建
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"MANUDebuggingLog\{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));    //获得文件夹路径
                }
 
                if (streamWriter == null)
                {
                    streamWriter = !File.Exists(directPath) ? File.CreateText(directPath) : File.AppendText(directPath);    //判断文件是否存在如果不存在则创建，如果存在则添加。
                }

                if (!string.IsNullOrEmpty(message))
                {
                    streamWriter.WriteLine(string.Format(@"{0}{1}  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "  " + message));
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                    streamWriter = null;
                }
            }

        }



    }
}
