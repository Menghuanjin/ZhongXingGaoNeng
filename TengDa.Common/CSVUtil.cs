using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TengDa.Common
{
  public   class CSVUtil
    {

        //yyyy/MM/dd-HH:mm:ss:ff
        public static void SaveInfoToCSVFileAtLog(string date, string status, string name, string content, string statusCode,string reInfo)
        {
            List<string[]> l = new List<string[]>();
            List<string[]> info = new List<string[]>();
            string tempPathMM = DateTime.Now.ToString("yyyyMMdd");
            string[] title = new string[] { "序号", "日期", "MES状态", "调用接口名称", "上传内容", "MES返回状态码", "MES返回信息" };
            string filePath =string .Format( @"D:\MESLog");
            CsvPath = filePath;
            if (!System.IO.File.Exists(string.Format(@"{0}\{1}.csv", filePath, tempPathMM)))
            {
                l.Add(title);
                string[] inputSS = { "1", date, status, name, content, statusCode, reInfo };
                info.Add(inputSS);
                try
                {
                    WriteInfoToCSVFileDisMath(info, l, false);
                }
                catch {}      
            }
            else
            {
                l = null;          
                try
                {
                    string[] inputSS = { Increase(), date, status, name, content, statusCode, reInfo };
                    info.Add(inputSS);
                    WriteInfoToCSVFileDisMath(info, l, false);
                }
                catch { }
            }
        }
        private static string csvpath;
        /// <summary>
        ///路径
        /// </summary>
        public static string CsvPath
        {
            get { return csvpath; }
            set { csvpath = value; }
        }

        private static string _var;

        public static string Var
        {
            get { return _var; }
            set { _var = value; }
        }


        private static string Increase()
        {
            DateTime now = DateTime.Now;
            string str1 = now.ToString("yyyyMMdd");
            string str2 = string.Format(@"{0}", saveLogFilePath + "\\" + (str1 + ".csv"));
            List<string[]> list = ReadCSV(str2);
            return (Convert.ToInt32(list[list.Count - 1][0])+1).ToString();
        }

        //D:\MESLog\20180830.csv
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="title"></param>
        /// <param name="isAtMath"></param>
        public static void WriteInfoToCSVFileDisMath(List<string[]> info, List<string[]> title, bool isAtMath)
        {
            if (!Directory.Exists(saveLogFilePath))//判断是否有此目录
                Directory.CreateDirectory(saveLogFilePath);//没有就创建
            DateTime now = DateTime.Now;
            string str1 = now.ToString("yyyyMM");
            if (!isAtMath)
            {
                now = DateTime.Now;
                str1 = now.ToString("yyyyMMdd");
            }
            string str2 = saveLogFilePath + "\\" + (str1 + ".csv");
            if (!File.Exists(str2) && title != null)
                WriteCSV(str2, title);
            WriteCSV(str2, true, info);
        }
        private static string saveLogFilePath
        {
            get
            {
                try
                {
                    string startupPath = CsvPath;//在哪一个盘路径
                    return string.Format("{0}", (object)startupPath);//文件夹名
                }
                catch
                {
                }
                return Application.StartupPath;
            }
            set
            {
                saveLogFilePath = value;
            }
        }
        public static void WriteCSV(string filePathName, List<string[]> ls)
        {
            WriteCSV(filePathName, false, ls);
        }

        public static void WriteCSV(string filePathName, bool append, List<string[]> ls)
        {
            StreamWriter streamWriter = new StreamWriter(filePathName, append, Encoding.Default);
            foreach (string[] strArray in ls)
                streamWriter.WriteLine(string.Join(",", strArray));
            streamWriter.Flush();
            streamWriter.Close();
        }

        public static List<string[]> ReadCSV(string filePathName)
        {
            List<string[]> list = new List<string[]>();
            using (StreamReader streamReader = new StreamReader(filePathName, Encoding.UTF8))
            {
                string str = "";
                while (str != null)
                {
                    str = streamReader.ReadLine();
                    if (str != null && str.Length > 0)
                        list.Add(str.Split(','));
                }
                streamReader.Close();
                return list;
            }
        }
        /// <summary>
        /// 导出excel  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Ilist集合</param>
        /// <param name="filepath">保存的地址</param>
        public static bool Export<T>(IList<T> data,  string filepath)
        {
            try
            {
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                Aspose.Cells.Worksheet sheet = (Aspose.Cells.Worksheet)workbook.Worksheets[0];
                PropertyInfo[] ps = typeof(T).GetProperties();
                var colIndex = "A";              
                foreach (var p in ps)
                {
                   sheet.Cells[colIndex + 1].PutValue(p.Name);//设置表头名称  要求表头为中文所以不用 p.name 为字段名称 可在list第一条数据为表头名称
                    int i = 2;
                    foreach (var d in data)
                    {
                        sheet.Cells[colIndex + i].PutValue(p.GetValue(d, null));
                        i++;
                    }
                    colIndex = getxls_top(colIndex); //((char)(colIndex[0] + 1)).ToString();//表头  A1/A2/
                }
                workbook.Save(filepath);
                GC.Collect();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        /// <summary>
        /// 生成新的对应的列  A-Z  AA-ZZ
        /// </summary>
        /// <param name="top">当前列</param>
        /// <returns></returns>
        private static string getxls_top(string top)
        {
            char[] toplist = top.ToArray();
            var itemtop = top.Last();
            string topstr = string.Empty;
            if ((char)itemtop == 90)//最后一个是Z
            {
                if (toplist.Count() == 1)
                {
                    topstr = "AA";
                }
                else
                {
                    toplist[0] = (char)(toplist[0] + 1);
                    toplist[toplist.Count() - 1] = 'A';
                    foreach (var item in toplist)
                    {
                        topstr += item.ToString();
                    }
                }
            }
            else//最后一个不是Z  包括top为两个字符
            {
                itemtop = (char)(itemtop + 1);
                toplist[toplist.Count() - 1] = itemtop;

                foreach (var item in toplist)
                {
                    topstr += item.ToString();
                }
            }
            return topstr;
        }
    }
}
