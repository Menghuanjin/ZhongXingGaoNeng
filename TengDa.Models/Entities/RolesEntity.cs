using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.Model;

namespace TengDa.Models.Entities
{
    [Serializable]
    public class RolesEntity : BaseModel
    {
        #region Model
        private int _roleid;
        private string _rolename;
        private string _permissioncodes;
        private DateTime? _createtime;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int RoleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PermissionCodes
        {
            set { _permissioncodes = value; }
            get { return _permissioncodes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
    [Serializable]
    public class RolesEntityORMMapper : ClassMapper<RolesEntity>
    {
        public RolesEntityORMMapper()
        {
            base.Table("Roles");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
