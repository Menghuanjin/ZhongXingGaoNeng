using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TengDa.Models.Entities;

namespace TengDa.Core
{
    public class PermissionNode
    {
        private IList<PermissionNode> children;

        public string Text { get; set; }

        public string Code { get; set; }

        public string Icon { get; set; }

        public int FontSize { get; set; }
        public string FontWeight { get; set; }

        public IList<PermissionNode> Children
        {
            get
            {
                return this.children ?? (this.children = (IList<PermissionNode>)new List<PermissionNode>());
            }
            set
            {
                this.children = value;
            }
        }
        public static string StringToUnicode(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                // 取两个字符，每个字符都是右对齐。
                stringBuilder.AppendFormat("u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Unicode编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string EnUnicode(string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    strResult.Append("\\u");
                    strResult.Append(((int)str[i]).ToString("x"));
                }
            }
            return strResult.ToString();
        }
        /// <summary>
        /// Unicode解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeUnicode1(string str)
        {
            //最直接的方法Regex.Unescape(str);
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            return reg.Replace(str, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        }
       static List<string> ios = new List<string>();
        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <param name="permissonModels"></param>
        /// <returns></returns>
        public static IList<PermissionNode> BuildPermissionNodes(IList<PermissionsEntity> permissonModels)
        {
            List<PermissionNode> permissionNodeList = new List<PermissionNode>();


            // string[] ios = new string[] { "\ue622", "\ue604", "\ue63f","\ue608","\ue61d","\ue604","\ue632","\ue610" };
            // { "\ue622", "\ue604", "\ue63f", "\ue608", "\ue61d", "\ue604", "\ue632", "\ue610" };
            foreach (PermissionsEntity permissionModel1 in permissonModels.Where<PermissionsEntity>((Func<PermissionsEntity, bool>)(m => !m.ParentId.HasValue)))
            {
                PermissionsEntity tm = permissionModel1;
                PermissionNode permissionNode = new PermissionNode();
                permissionNode.Code = tm.PermissionCode;
                permissionNode.Text = tm.PermissionName;
                if (!string.IsNullOrEmpty(tm.IconSource))
                {
                    string ico = string.Empty;
                    if (!string.IsNullOrEmpty(permissionModel1.IconSource))
                    {
                        ico = PermissionNode.DeUnicode1(permissionModel1.IconSource);
                    }
                    permissionNode.Icon = ico;//ico;                  
                }
                else
                {
                    // permissionNode.Icon = ios[0];
                }

                //permissionNode.FontSize = 16;
                permissionNode.FontWeight = "Bold";


         
                foreach (PermissionsEntity permissionModel2 in permissonModels.Where<PermissionsEntity>((Func<PermissionsEntity, bool>)(m =>
                {
                    int? parentId = m.ParentId;
                    int permissionId = tm.PermissionId;
                    if (parentId.GetValueOrDefault() != permissionId)
                    {
                        return false;
                    }
                    return parentId.HasValue;
                })).ToList<PermissionsEntity>())
                    permissionNode.Children.Add(new PermissionNode()
                    {
                        Code = permissionModel2.PermissionCode,
                        Text = permissionModel2.PermissionName,
                        Icon = string.IsNullOrEmpty(permissionModel2.IconSource) ? " " : PermissionNode.DeUnicode1(permissionModel2.IconSource),
                        FontWeight = "Normal"
                    });
                permissionNodeList.Add(permissionNode);
            }

            return (IList<PermissionNode>)permissionNodeList;
        }
      
    }
}
