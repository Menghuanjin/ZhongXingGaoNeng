using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.Models.Entities;
using TengDa.Core;
using System.Collections.ObjectModel;
using TengDa.Model.Entities;
using System.Text.RegularExpressions;
using TengDa.Model;

namespace TengDa.Core
{
    public class AppContext
    {
        private static AppContext current = new AppContext();
        public static AppContext Current
        {
            get
            {
                return AppContext.current;
            }
        }

        public UsersEntity UserInfo { get; set; }

        public ObservableCollection<RolesEntity> RoleData { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public int[] UserRoleListArray;
        private List<string> permissionCodes;
        public TengDa.DB.PermissionsDB permissionsDB = new DB.PermissionsDB();
        /// <summary>
        /// mes配置
        /// </summary>
        public MesConfigure MesModel { get; set; }
        public List<string> PermissionCodes
        {
            get
            {
                return this.permissionCodes ?? (this.permissionCodes = new List<string>());
            }
            set
            {
                this.permissionCodes = value;
            }
        }

        public List<PermissionNode> GetPermissionNodesAsync()
        {
       
            return this.BuildUserPermissionNodes(PermissionNode.BuildPermissionNodes(permissionsDB.GetAll<PermissionsEntity>().ToList()));
        }

        private List<PermissionNode> BuildUserPermissionNodes(IList<PermissionNode> permissionNodes)
        {
            List<PermissionNode> permissionNodeList1 = new List<PermissionNode>();
            foreach (PermissionNode permissionNode1 in (IEnumerable<PermissionNode>)permissionNodes)
            {
                List<PermissionNode> permissionNodeList2 = this.BuildUserPermissionNodes(permissionNode1.Children);
                if (permissionNodeList2.Count > 0 || string.IsNullOrEmpty(permissionNode1.Code) || this.PermissionCodes.Contains(permissionNode1.Code))
                {
                    PermissionNode permissionNode2 = new PermissionNode()
                    {
                        FontWeight = permissionNode1.FontWeight,
                        Icon = permissionNode1.Icon,// string.IsNullOrEmpty(permissionNode1.Icon) ? " " : PermissionNode.DeUnicode1(permissionNode1.Icon),//DeUnicode1(),
                        Text = permissionNode1.Text,
                        Code = permissionNode1.Code,
                        Children = (IList<PermissionNode>)permissionNodeList2 };
                    permissionNodeList1.Add(permissionNode2);
                }
            }
            return permissionNodeList1;
        }
        public  string DeUnicode1(string str)
        {
            //最直接的方法Regex.Unescape(str);
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            return reg.Replace(str, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        }



    }
}
