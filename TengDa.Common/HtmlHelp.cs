using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TengDa.Common
{
    public class HtmlHelp
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<Data> datas = new List<Data>();//定义1个列表用于保存结果
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="Url">路径</param>
        public static void LoadData(string Url)
        {
            try
            {
                string strHTML = "";
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(Url);
                StreamReader sr = new StreamReader(myStream, Encoding.UTF8);//注意编码
                strHTML = sr.ReadToEnd();
                myStream.Close();
                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(strHTML);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载

                HtmlNodeCollection collection = htmlDocument.DocumentNode.SelectSingleNode("html/body/div/ul").ChildNodes;//跟Xpath一样，轻松的定位到相应节点下
                foreach (HtmlNode node in collection)
                {
                    //去除\r\n以及空格，获取到相应td里面的数据
                    string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //如果符合条件，就加载到对象列表里面
                    if (line.Length > 0)
                    {
                        datas.Add(new Data() { icon = line[0], name = line[1], code = line[2], fontclass = line[3] });
                    }
                }
            }
            catch{}
        }
        public static Data GetInfoByName(string name)
        {
            var data = datas.Where(a => a.name.Contains(name)).ToList().FirstOrDefault();
            return data;
        }
        public class Data
        {
            public string name { get; set; }

            public string code { get; set; }

            public string fontclass { get; set; }

            public string icon { get; set; }
        }
    }
    }
