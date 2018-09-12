using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RobotDemo
{
    public class XmlHelp
    {
        public static List<string> IpAndPort(string xml = @"C:\Users\DELL\Desktop\RobotDemo\XML文件\VisionSysemt.xml")
        {
            XmlDocument doc = new XmlDocument();
            List<string> List = new List<string>();
            string[] strlist = new string[] { "IP", "PORT" };
            doc.Load(xml);
            XmlElement root = null;
            root = doc.DocumentElement;
            XmlNodeList node = null;
            for (int i = 0; i < 2; i++)
            {
                node = root.SelectNodes("/ETHERNETKRL/CONFIGURATION/EXTERNAL/" + strlist[i]);
                foreach (XmlNode item in node)
                {
                    List.Add(item.InnerText);
                    string str = item.InnerText;//IP
                }
            }
            return List;
        }
        public static List<string> InstructAddress(string xml = @"C:\Users\DELL\Desktop\RobotDemo\XML文件\VisionSysemt.xml")
        {
            XmlDocument doc = new XmlDocument();
            List<string> List = new List<string>();
            string[] strlist = new string[] { "IP", "PORT" };
            doc.Load(xml);
            XmlElement root = null;
            root = doc.DocumentElement;
            XmlNodeList node = null;

            node = root.SelectNodes("/ETHERNETKRL/RECEIVE/XML/ELEMENT");

            foreach (XmlNode item in node)
            {
                List.Add(item.OuterXml);
                string str = item.InnerText;//IP
            }

            return List;
        }

        #region 公有属性
        private string _XMLPath;
        public string XMLPath
        {
            get { return this._XMLPath; }
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 导入XML文件
        /// </summary>
        /// <param name="XMLPath">XML文件路径</param>
        private XmlDocument XMLLoad()
        {
            string XMLFile = XMLPath;
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;
                if (File.Exists(filename)) xmldoc.Load(filename);
            }
            catch (Exception e)
            { }
            return xmldoc;
        }

        /// <summary>
        /// 导入XML文件
        /// </summary>
        /// <param name="XMLPath">XML文件路径</param>
        private static XmlDocument XMLLoad(string strPath)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + strPath;
                if (File.Exists(filename)) xmldoc.Load(filename);
            }
            catch (Exception e)
            { }
            return xmldoc;
        }

        /// <summary>
        /// 返回完整路径
        /// </summary>
        /// <param name="strPath">Xml的路径</param>
        private static string GetXmlFullPath(string strPath)
        {
            if (strPath.IndexOf(":") > 0)
            {
                return strPath;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.MapPath(strPath);
            }
        }
        #endregion

        #region 读取数据
        /// <summary>
        /// 读取指定节点的数据
        /// </summary>
        /// <param name="node">节点</param>
        /// 使用示列:
        /// XMLProsess.Read("/Node", "")
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
        public string Read(string node)
        {
            string value = "";
            try
            {
                XmlDocument doc = XMLLoad();
                XmlNode xn = doc.SelectSingleNode(node);
                value = xn.InnerText;
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 读取指定路径和节点的串联值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// 使用示列:
        /// XMLProsess.Read(path, "/Node", "")
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']")
        public static string Read(string path, string node)
        {
            string value = "";
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = xn.InnerText;
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 读取指定路径和节点的属性值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// 使用示列:
        /// XMLProsess.Read(path, "/Node", "")
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        public string[] ReadAllChildallValue(string node)
        {
            int i = 0;
            string[] str = { };
            XmlDocument doc = XMLLoad();
            XmlNode xn = doc.SelectSingleNode(node);
            XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
            if (nodelist.Count > 0)
            {
                str = new string[nodelist.Count];
                foreach (XmlElement el in nodelist)//读元素值
                {
                    str[i] = el.Value;
                    i++;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        public XmlNodeList ReadAllChild(string node)
        {
            XmlDocument doc = XMLLoad();
            XmlNode xn = doc.SelectSingleNode(node);
            XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
            return nodelist;
        }

        /// <summary> 
        /// 读取XML返回经排序或筛选后的DataView
        /// </summary>
        /// <param name="strWhere">筛选条件，如:"name='kgdiwss'"</param>
        /// <param name="strSort"> 排序条件，如:"Id desc"</param>
        public DataView GetDataViewByXml(string strWhere, string strSort)
        {
            try
            {
                string XMLFile = this.XMLPath;
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;
                DataSet ds = new DataSet();
                ds.ReadXml(filename);
                DataView dv = new DataView(ds.Tables[0]); //创建DataView来完成排序或筛选操作    
                if (strSort != null)
                {
                    dv.Sort = strSort; //对DataView中的记录进行排序
                }
                if (strWhere != null)
                {
                    dv.RowFilter = strWhere; //对DataView中的记录进行筛选，找到我们想要的记录
                }
                return dv;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 读取XML返回DataSet
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        public DataSet GetDataSetByXml(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node", "Element", "", "Value")
        /// XMLProsess.Insert(path, "/Node", "Element", "Attribute", "Value")
        /// XMLProsess.Insert(path, "/Node", "", "Attribute", "Value")
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="strList">由XML属性名和值组成的二维数组</param>
        public static void Insert(string path, string node, string element, string[][] strList)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = doc.CreateElement(element);
                string strAttribute = "";
                string strValue = "";
                for (int i = 0; i < strList.Length; i++)
                {
                    for (int j = 0; j < strList[i].Length; j++)
                    {
                        if (j == 0)
                            strAttribute = strList[i][j];
                        else
                            strValue = strList[i][j];
                    }
                    if (strAttribute.Equals(""))
                        xe.InnerText = strValue;
                    else
                        xe.SetAttribute(strAttribute, strValue);
                }
                xn.AppendChild(xe);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 插入一行数据
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        /// <param name="Columns">要插入行的列名数组，如：string[] Columns = {"name","IsMarried"};</param>
        /// <param name="ColumnValue">要插入行每列的值数组，如：string[] ColumnValue={"XML大全","false"};</param>
        /// <returns>成功返回true,否则返回false</returns>
        public static bool WriteXmlByDataSet(string strXmlPath, string[] Columns, string[] ColumnValue)
        {
            try
            {
                //根据传入的XML路径得到.XSD的路径，两个文件放在同一个目录下
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath)); //读XML架构，关系到列的数据类型
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();                 //在原来的表格基础上创建新行
                for (int i = 0; i < Columns.Length; i++)      //循环给一行中的各个列赋值
                {
                    newRow[Columns[i]] = ColumnValue[i];
                }
                dt.Rows.Add(newRow);
                dt.AcceptChanges();
                ds.AcceptChanges();
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改指定节点的数据
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        public void Update(string node, string value)
        {
            try
            {
                XmlDocument doc = XMLLoad();
                XmlNode xn = doc.SelectSingleNode(node);
                xn.InnerText = value;
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLPath);
            }
            catch { }
        }

        /// <summary>
        /// 修改指定节点的数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node","Value")
        /// XMLProsess.Insert(path, "/Node","Value")
        public static void Update(string path, string node, string value)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                xn.InnerText = value;
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 修改指定节点的属性值(静态)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node", "", "Value")
        /// XMLProsess.Insert(path, "/Node", "Attribute", "Value")
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 更改符合条件的一条记录
        /// </summary>
        /// <param name="strXmlPath">XML文件路径</param>
        /// <param name="Columns">列名数组</param>
        /// <param name="ColumnValue">列值数组</param>
        /// <param name="strWhereColumnName">条件列名</param>
        /// <param name="strWhereColumnValue">条件列值</param>
        public static bool UpdateXmlRow(string strXmlPath, string[] Columns, string[] ColumnValue, string strWhereColumnName, string strWhereColumnValue)
        {
            try
            {
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath));//读XML架构，关系到列的数据类型
                ds.ReadXml(GetXmlFullPath(strXmlPath));

                //先判断行数
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果当前记录为符合Where条件的记录
                        if (ds.Tables[0].Rows[i][strWhereColumnName].ToString().Trim().Equals(strWhereColumnValue))
                        {
                            //循环给找到行的各列赋新值
                            for (int j = 0; j < Columns.Length; j++)
                            {
                                ds.Tables[0].Rows[i][Columns[j]] = ColumnValue[j];
                            }
                            ds.AcceptChanges();                     //更新DataSet
                            ds.WriteXml(GetXmlFullPath(strXmlPath));//重新写入XML文件
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除节点值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Delete(path, "/Node", "")
        /// XMLProsess.Delete(path, "/Node", "Attribute")
        public static void Delete(string path, string node)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                xn.ParentNode.RemoveChild(xn);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Delete(path, "/Node", "")
        /// XMLProsess.Delete(path, "/Node", "Attribute")
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 删除所有行
        /// </summary>
        /// <param name="strXmlPath">XML路径</param>
        public static bool DeleteXmlAllRows(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.Clear();
                }
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 通过删除DataSet中指定索引行，重写XML以实现删除指定行
        /// </summary>
        /// <param name="iDeleteRow">要删除的行在DataSet中的Index值</param>
        public static bool DeleteXmlRowByIndex(string strXmlPath, int iDeleteRow)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows[iDeleteRow].Delete();
                }
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定列中指定值的行
        /// </summary>
        /// <param name="strXmlPath">XML相对路径</param>
        /// <param name="strColumn">列名</param>
        /// <param name="ColumnValue">指定值</param>
        public static bool DeleteXmlRows(string strXmlPath, string strColumn, string[] ColumnValue)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //判断行多还是删除的值多，多的for循环放在里面
                    if (ColumnValue.Length > ds.Tables[0].Rows.Count)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < ColumnValue.Length; j++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < ColumnValue.Length; j++)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    ds.WriteXml(GetXmlFullPath(strXmlPath));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 对象定义  

        private XmlDocument xmlDoc = new XmlDocument();
        XmlNode xmlnode;
        XmlElement xmlelem;

        #endregion

        #region 属性定义  

        private string errorMess;
        public string ErrorMess
        {
            get { return errorMess; }
            set { errorMess = value; }
        }

        private string filePath;
        public string FilePath
        {
            set { filePath = value; }
            get { return filePath; }
        }

        #endregion

        #region 创建XML操作对象  
        /// <summary>  
        /// 创建XML操作对象  
        /// </summary>  
        public void OpenXML()
        {
            try
            {
                if (!string.IsNullOrEmpty(FilePath))
                {
                    xmlDoc.Load(filePath);
                }
                else
                {
                    FilePath = string.Format(@"E:\log4net.xml"); //默认路径  
                    xmlDoc.Load(filePath);
                }
            }
            catch (Exception ex)
            {
                ErrorMess = ex.Message;
            }
        }
        #endregion

        #region 创建Xml 文档  
        /// <summary>  
        /// 创建一个带有根节点的Xml 文件  
        /// </summary>  
        /// <param name="FileName">Xml 文件名称</param>  
        /// <param name="rootName">根节点名称</param>  
        /// <param name="Encode">编码方式:gb2312，UTF-8 等常见的</param>  
        /// <param name="DirPath">保存的目录路径</param>  
        /// <returns></returns>  
        public bool CreatexmlDocument(string FileName, string rootName, string Encode)
        {
            try
            {
                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", Encode, null);
                xmlDoc.AppendChild(xmldecl);
                xmlelem = xmlDoc.CreateElement("", rootName, "");
                xmlDoc.AppendChild(xmlelem);
                xmlDoc.Save(FileName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        //获取值  

        #region 得到表  
        /// <summary>  
        /// 得到表  
        /// </summary>  
        /// <returns></returns>  
        public DataView GetData()
        {
            xmlDoc = new XmlDocument();
            DataSet ds = new DataSet();
            StringReader read = new StringReader(xmlDoc.SelectSingleNode(FilePath).OuterXml);
            ds.ReadXml(read);
            return ds.Tables[0].DefaultView;
        }
        #endregion

        #region 读取指定节点的指定属性值  
        /// <summary>  
        /// 功能:  
        /// 读取指定节点的指定属性值  
        /// </summary>  
        /// <param name="strNode">节点名称(相对路径：//+节点名称)</param>  
        /// <param name="strAttribute">此节点的属性</param>  
        /// <returns></returns>  
        public string GetXmlNodeValue(string strNode, string strAttribute)
        {
            string strReturn = "";
            try
            {
                //根据指定路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                //获取节点的属性，并循环取出需要的属性值  
                XmlAttributeCollection xmlAttr = xmlNode.Attributes;

                for (int i = 0; i < xmlAttr.Count; i++)
                {
                    if (xmlAttr.Item(i).Name == strAttribute)
                    {
                        strReturn = xmlAttr.Item(i).Value;
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        #endregion

        #region 读取指定节点的值  
        /// <summary>  
        /// 功能:  
        /// 读取指定节点的值  
        /// </summary>  
        /// <param name="strNode">节点名称</param>  
        /// <returns></returns>  
        public string GetXmlNodeValue(string strNode)
        {
            string strReturn = String.Empty;
            try
            {
                //根据路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                strReturn = xmlNode.InnerText;
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strReturn;
        }
        #endregion

        #region 获取XML文件的根元素  
        /// <summary>  
        /// 获取XML文件的根元素  
        /// </summary>  
        public XmlNode GetXmlRoot()
        {
            return xmlDoc.DocumentElement;
        }
        #endregion

        #region 获取XML节点值  
        /// <summary>  
        /// 获取XML节点值  
        /// </summary>  
        /// <param name="nodeName"></param>  
        /// <returns></returns>  
        public string GetNodeValue(string nodeName)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);

            XmlNodeList xnl = xmlDoc.ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement)xnf;
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    if (xn2.InnerText == nodeName)
                    {
                        XmlElement xe2 = (XmlElement)xn2;
                        return xe2.GetAttribute("value");
                    }
                }
            }
            return null;
        }
        #endregion

        //添加或插入  

        #region 设置节点值  
        /// <summary>  
        /// 功能:  
        /// 设置节点值  
        /// </summary>  
        /// <param name="strNode">节点的名称</param>  
        /// <param name="newValue">节点值</param>  
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeValue)
        {
            try
            {
                //根据指定路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlNodePath);
                //设置节点值  
                xmlNode.InnerText = xmlNodeValue;
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 添加父节点  

        /// <summary>  
        /// 在根节点下添加父节点  
        /// </summary>  
        public void AddParentNode(string parentNode)
        {
            XmlNode root = GetXmlRoot();
            XmlNode parentXmlNode = xmlDoc.CreateElement(parentNode);

            root.AppendChild(parentXmlNode);
        }
        #endregion

        #region 向一个已经存在的父节点中插入一个子节点  
        /// <summary>  
        /// 向一个已经存在的父节点中插入一个子节点  
        /// </summary>  
        public void AddChildNode(string parentNodePath, string childNodePath)
        {
            XmlNode parentXmlNode = xmlDoc.SelectSingleNode(parentNodePath);
            XmlNode childXmlNode = xmlDoc.CreateElement(childNodePath);

            parentXmlNode.AppendChild(childXmlNode);
        }
        #endregion

        #region 向一个节点添加属性  
        /// <summary>  
        /// 向一个节点添加属性  
        /// </summary>  
        public void AddAttribute(string NodePath, string NodeAttribute)
        {
            XmlAttribute nodeAttribute = xmlDoc.CreateAttribute(NodeAttribute);
            XmlNode nodePath = xmlDoc.SelectSingleNode(NodePath);
            nodePath.Attributes.Append(nodeAttribute);
        }
        #endregion

        #region 插入一个节点和它的若干子节点  
        /// <summary>  
        /// 插入一个节点和它的若干子节点  
        /// </summary>  
        /// <param name="NewNodeName">插入的节点名称</param>  
        /// <param name="HasAttributes">此节点是否具有属性，True 为有，False 为无</param>  
        /// <param name="fatherNode">此插入节点的父节点</param>  
        /// <param name="htAtt">此节点的属性，Key 为属性名，Value 为属性值</param>  
        /// <param name="htSubNode"> 子节点的属性， Key 为Name,Value 为InnerText</param>  
        /// <returns>返回真为更新成功，否则失败</returns>  
        public bool InsertNode(string NewNodeName, bool HasAttributes, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                xmlDoc.Load(FilePath);
                XmlNode root = xmlDoc.SelectSingleNode(fatherNode);
                xmlelem = xmlDoc.CreateElement(NewNodeName);
                if (htAtt != null && HasAttributes)//若此节点有属性，则先添加属性  
                {
                    SetAttributes(xmlelem, htAtt);
                    AddNodes(xmlelem.Name, xmlDoc, xmlelem, htSubNode);//添加完此节点属性后，再添加它的子节点和它们的InnerText  
                }
                else
                {
                    AddNodes(xmlelem.Name, xmlDoc, xmlelem, htSubNode);//若此节点无属性，那么直接添加它的子节点  
                }
                root.AppendChild(xmlelem);
                xmlDoc.Save(FilePath);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 设置节点属性  
        /// <summary>  
        /// 设置节点属性  
        /// </summary>  
        /// <param name="xe">节点所处的Element</param>  
        /// <param name="htAttribute">节点属性，Key 代表属性名称，Value 代表属性值</param>  
        public void SetAttributes(XmlElement xe, Hashtable htAttribute)
        {
            foreach (DictionaryEntry de in htAttribute)
            {
                xe.SetAttribute(de.Key.ToString(), de.Value.ToString());
            }
        }
        #endregion

        #region 增加子节点到根节点下  
        /// <summary>  
        /// 增加子节点到根节点下  
        /// </summary>  
        /// <param name="rootNode">上级节点名称</param>  
        /// <param name="xmlDoc">Xml 文档</param>  
        /// <param name="rootXe">父根节点所属的Element</param>  
        /// <param name="SubNodes">子节点属性，Key 为Name 值，Value 为InnerText 值</param>  
        public void AddNodes(string rootNode, XmlDocument xmlDoc, XmlElement rootXe, Hashtable SubNodes)
        {
            foreach (DictionaryEntry de in SubNodes)
            {
                xmlnode = xmlDoc.SelectSingleNode(rootNode);
                XmlElement subNode = xmlDoc.CreateElement(de.Key.ToString());
                subNode.InnerText = de.Value.ToString();
                rootXe.AppendChild(subNode);
            }
        }
        #endregion

        //更新  

        #region 设置节点的属性值  
        /// <summary>  
        /// 功能:  
        /// 设置节点的属性值  
        /// </summary>  
        /// <param name="xmlNodePath">节点名称</param>  
        /// <param name="xmlNodeAttribute">属性名称</param>  
        /// <param name="xmlNodeAttributeValue">属性值</param>  
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeAttribute, string xmlNodeAttributeValue)
        {
            try
            {
                //根据指定路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlNodePath);

                //获取节点的属性，并循环取出需要的属性值  
                XmlAttributeCollection xmlAttr = xmlNode.Attributes;
                for (int i = 0; i < xmlAttr.Count; i++)
                {
                    if (xmlAttr.Item(i).Name == xmlNodeAttribute)
                    {
                        xmlAttr.Item(i).Value = xmlNodeAttributeValue;
                        break;
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        #endregion

        #region 更新节点  
        /// <summary>  
        /// 更新节点  
        /// </summary>  
        /// <param name="fatherNode">需要更新节点的上级节点</param>  
        /// <param name="htAtt">需要更新的属性表，Key 代表需要更新的属性，Value 代表更新后的值</param>  
        /// <param name="htSubNode">需要更新的子节点的属性表，Key 代表需要更新的子节点名字Name,Value 代表更新后的值InnerText</param>  
        /// <returns>返回真为更新成功，否则失败</returns>  
        public bool UpdateNode(string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(FilePath);
                XmlNodeList root = xmlDoc.SelectSingleNode(fatherNode).ChildNodes;
                UpdateNodes(root, htAtt, htSubNode);
                xmlDoc.Save(FilePath);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 更新节点属性和子节点InnerText 值  
        /// <summary>  
        /// 更新节点属性和子节点InnerText 值  
        /// </summary>  
        /// <param name="root">根节点名字</param>  
        /// <param name="htAtt">需要更改的属性名称和值</param>  
        /// <param name="htSubNode">需要更改InnerText 的子节点名字和值</param>  
        public void UpdateNodes(XmlNodeList root, Hashtable htAtt, Hashtable htSubNode)
        {
            foreach (XmlNode xn in root)
            {
                xmlelem = (XmlElement)xn;
                if (xmlelem.HasAttributes)//如果节点如属性，则先更改它的属性  
                {
                    foreach (DictionaryEntry de in htAtt)//遍历属性哈希表  
                    {
                        if (xmlelem.HasAttribute(de.Key.ToString()))//如果节点有需要更改的属性  
                        {
                            xmlelem.SetAttribute(de.Key.ToString(), de.Value.ToString());//则把哈希表中相应的值Value 赋给此属性Key  
                        }
                    }
                }
                if (xmlelem.HasChildNodes)//如果有子节点，则修改其子节点的InnerText  
                {
                    XmlNodeList xnl = xmlelem.ChildNodes;
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement xe = (XmlElement)xn1;
                        foreach (DictionaryEntry de in htSubNode)
                        {
                            if (xe.Name == de.Key.ToString())//htSubNode 中的key 存储了需要更改的节点名称，  
                            {
                                xe.InnerText = de.Value.ToString();//htSubNode中的Value存储了Key 节点更新后的数据  
                            }
                        }
                    }
                }
            }
        }
        #endregion

        //删除  

        #region 删除一个节点的属性  
        /// <summary>  
        /// 删除一个节点的属性  
        /// </summary>  
        public void DeleteAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            XmlNodeList nodePath = xmlDoc.SelectSingleNode(NodePath).ChildNodes;

            foreach (XmlNode xn in nodePath)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.GetAttribute(NodeAttribute) == NodeAttributeValue)
                {
                    xe.RemoveAttribute(NodeAttribute);//删除属性  
                }
            }
        }

        #endregion

        #region 删除一个节点  
        /// <summary>  
        /// 删除一个节点  
        /// </summary>  
        public void DeleteXmlNode(string tempXmlNode)
        {
            XmlNode xmlNodePath = xmlDoc.SelectSingleNode(tempXmlNode);
            xmlNodePath.ParentNode.RemoveChild(xmlNodePath);
        }

        #endregion

        #region 删除指定节点下的子节点  
        /// <summary>  
        /// 删除指定节点下的子节点  
        /// </summary>  
        /// <param name="fatherNode">制定节点</param>  
        /// <returns>返回真为更新成功，否则失败</returns>  
        public bool DeleteNodes(string fatherNode)
        {
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(FilePath);
                xmlnode = xmlDoc.SelectSingleNode(fatherNode);
                xmlnode.RemoveAll();
                xmlDoc.Save(FilePath);
                return true;
            }
            catch (XmlException xe)
            {
                throw new XmlException(xe.Message);
            }
        }
        #endregion

        //内部函数与保存  

        #region 私有函数  

        private string functionReturn(XmlNodeList xmlList, int i, string nodeName)
        {
            string node = xmlList[i].ToString();
            string rusultNode = "";
            for (int j = 0; j < i; j++)
            {
                if (node == nodeName)
                {
                    rusultNode = node.ToString();
                }
                else
                {
                    if (xmlList[j].HasChildNodes)
                    {
                        functionReturn(xmlList, j, nodeName);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return rusultNode;


        }

        #endregion

        #region 保存XML文件  
        /// <summary>  
        /// 功能:   
        /// 保存XML文件  
        ///   
        /// </summary>  
        public void SavexmlDocument()
        {
            try
            {
                xmlDoc.Save(FilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>  
        /// 功能: 保存XML文件  
        /// </summary>  
        /// <param name="tempXMLFilePath"></param>  
        public void SavexmlDocument(string tempXMLFilePath)
        {
            try
            {
                xmlDoc.Save(tempXMLFilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
    }
    #endregion
}
