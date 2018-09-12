using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.Models;

namespace TengDa.Model.Entities
{
    [Serializable]
    public class UsersEntity : BaseModel
    {
        #region Model
        private int _userid;
        private string _usernumber;
        private string _userpwd;
        private string _username;
        private int _roleid;
        private string _gender;
        private DateTime? _lastlogintime;
        private DateTime? _createtime;
        private string _pagerole;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNumber
        {
            set { _usernumber = value; }
            get { return _usernumber; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd
        {
            set { _userpwd = value; }
            get { return _userpwd; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int RoleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            set { _gender = value; }
            get { return _gender; }
        }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 页面按钮权限
        /// </summary>
        public string pageRole
        {
            set { _pagerole = value; }
            get { return _pagerole; }
        }
        #endregion Model
    }
    [Serializable]
    public class UsersEntityORMMapper : ClassMapper<UsersEntity>
    {
        public UsersEntityORMMapper()
        {
            base.Table("Users");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
