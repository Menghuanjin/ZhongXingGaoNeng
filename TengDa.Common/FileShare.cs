using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;
using System.IO;


namespace RobotDemo
{
    /// <summary>
    /// 将远程连接的IP，将本机文件复制到远程连接的IP路径
    /// </summary>
    public class FileShare
    {
       public static bool connectState(string path)
        {
            return connectState(path, "", "");
        }
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }


        //read file
        public static void ReadFiles(string path)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);

                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        //write file
        public static void WriteFiles(string path)
        {
            try
            {
                // Create an instance of StreamWriter to write text to a file.
                // The using statement also closes the StreamWriter.
                using (StreamWriter sw = new StreamWriter(path))
                {
              

                    // Add some text to the file.
                    sw.Write("This is the ");
                    sw.WriteLine("header for the file.");
                    sw.WriteLine("-------------------");
                    // Arbitrary objects can also be written to the file.
                    sw.Write("The date is: ");
                    sw.WriteLine(DateTime.Now);
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }

    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// 将连接共享 (先将需要发送的文件的电脑的文件夹，需要设置共享，本地将指定路径发送文件夹)
        /// </summary>
        /// <param name="path">共享的IP + 共享文件夹的路径 格式(@"\\192.168.250.10\test")</param>
        /// <param name="file">需要发送的文件绝对路径 格式(@"C:\Users\DELL\Desktop\XML文件\VisionSysemt.xml")</param>
        /// <param name="userName">登录的账户</param>
        /// <param name="passWord">登录的密码</param>
        public bool Page_Load(string path, string file, string userName, string passWord)
        {
            bool status = false;

            //连接共享文件夹
            status = FileShare.connectState(path, userName, passWord);
            if (status)
            {
                DirectoryInfo theFolder = new DirectoryInfo(path);

                //先测试读文件，把目录路径与文件名连接
                string filename = theFolder.ToString() + "\\good.txt";
                FileShare.ReadFiles(filename);

                //测试写文件，拼出完整的路径
                filename = theFolder.ToString() + "\\VisionSysemt.xml";
                string origpath = System.IO.Path.GetFullPath(file);
                try
                {
                    File.Copy(origpath, filename, true);//将整个文件复制
                    //  FileShare.WriteFiles(filename);将文本类型写入数据
                }
                catch { return false; }
            }
            return true;   

            //    //遍历共享文件夹，把共享文件夹下的文件列表列到listbox
            //    foreach (FileInfo nextFile in theFolder.GetFiles())
            //    {

            //        string str = nextFile.Name;
            //     //   ListBox1.Items.Add(nextFile.Name);
            //    }
            //}
            //else
            //{
            //   // ListBox1.Items.Add("未能连接！");
            //}
        }
    }
}
