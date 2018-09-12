using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TengDa.Common;
using TengDa.Foundatoin;
using TengDa.Model;
using TengDa.Model.Entities;
using TengDa.Core;
using TengDa.Models.Entities;

namespace TengDa.ViewModel.VW
{
    public class LoginViewModel : ObservableObject
    {
        #region 属性
        /// <summary>
        /// 登录命令
        /// </summary>
        ICommand _loginCommand;
        /// <summary>
        /// 选择登录角色变更
        /// </summary>
        ICommand _selectedIndexChangedCommand;

        bool _canLogin = true;
        /// <summary>
        /// 登录选择角色 是否允许登录
        /// </summary>
        bool _selectRoleCanLogin = false;
        string _account = "admin";
        string _password = "admin";
        ///// <summary>
        ///// 当前选中的角色
        ///// </summary>
        //DropdownItem _selectedRole = DropdownItem.GetPlease;
        /// <summary>
        /// 数据访问层
        /// </summary>
        TengDa.DB.UsersDB user = new DB.UsersDB();

        TengDa.DB.RolesDB rolesDB = new DB.RolesDB();
        public string Account { get { return _account; } set { _account = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        //public string IP { get; set; }
        //public int ServicePort { get; set; }
        //public int UpdatePort { get; set; }       

        /// <summary>
        /// 角色列表
        /// </summary>
        //public List<DropdownItem> RoleList
        //{
        //    get
        //    {
        //        List<DropdownItem> list = new List<DropdownItem>();
        //        list.Add(DropdownItem.GetPlease);
        //        for (int i = 0; i < 3; i++)
        //        {
        //            DropdownItem item = new DropdownItem();
        //            item.ID = i + 1;
        //            item.Text = "用户" + i;
        //            list.Add(item);
        //        }
        //        foreach (RoleInfo item in User.Current.RoleList)
        //        {
        //            DropdownItem di = new DropdownItem();
        //            di.ID = item.ID;
        //            di.Text = item.Name;
        //            list.Add(di);
        //        }
        //        return list;
        //    }
        //    set
        //    {
        //        RoleList = value;
        //        base.RaisePropertyChanged("RoleList");
        //    }
        //}

        //public DropdownItem SelectedRole
        //{
        //    get
        //    {
        //        return this._selectedRole;
        //    }
        //    set
        //    {
        //        if (!this._selectedRole.Equals(value))
        //        {
        //            this._selectedRole = value;
        //            base.RaisePropertyChanged("SelectedRole");
        //        }
        //    }
        //}

        public bool CanLogin
        {
            get
            {
                return _canLogin;
            }
            set
            {
                _canLogin = value;
                base.RaisePropertyChanged("CanLogin");
            }
        }
        /// <summary>
        /// 选择角色登录 是否允许登录
        /// </summary>
        public bool SelectRoleCanLogin
        {
            get
            {
                return _selectRoleCanLogin;
            }
            set
            {
                _selectRoleCanLogin = value;
                base.RaisePropertyChanged("SelectRoleCanLogin");
            }
        }
        #endregion

        #region 事件
        //ViewModel里面的事件，一般来说都是通知View做相应的UI的操作
        public event GetResultEventHandle GetResult;

        #endregion

        #region 命令
        public ICommand LoginCommand
        {
            get
            {
                if (this._loginCommand == null)
                {
                    this._loginCommand = new RelayCommand(() => this.ExecLogin());
                }

                return this._loginCommand;
            }
        }

        //public ICommand SelectedIndexChangedCommand
        //{
        //    get
        //    {
        //        if (this._selectedIndexChangedCommand == null)
        //        {
        //            this._selectedIndexChangedCommand = new RelayCommand<object>(
        //                (t) => this.ExecSelectedIndexChanged(t),
        //                (t) => true);
        //        }

        //        return this._selectedIndexChangedCommand;
        //    }
        //}

        #endregion

        #region 对View可见的公共方法
        #endregion

        #region 私有方法
        #region 登录
        void ExecLogin()
        {

            Result rltModel = new Result();
            CanLogin = false;
            UsersEntity result = new UsersEntity();
            Action action = new Action(() =>
            {
                try
                {
                    result = user.GetUserLogin(Account, Password);
                    if (result != null)
                    {
                        User.Current.Credentials = new ClientCredentials() { Account = this.Account, Password = this.Password };
                        User.Current.ID = result.UserId;
                        User.Current.Name = result.UserName;
                        User.Current.UserNumber = result.UserNumber;
                        User.Current.RoleCode = result.RoleId.ToString();

                        AppContext.Current.UserInfo = result;
                        if (!string.IsNullOrEmpty(User.Current.RoleCode))
                        {
                            RolesEntity rolesModel = rolesDB.GetUserRolesById(result.RoleId);
                            if (rolesModel != null)
                            {
                                AppContext.Current.PermissionCodes.AddRange(rolesModel.PermissionCodes.Split(','));
                            }
                        }
                        //User.Current.SystemId = systemId;
                        //User.Current.RoleList = roleList;
                    }
                    else
                        rltModel.Message = "用户名或密码错误，请重新登录！";
                }
                catch (Exception ex)
                {
                    rltModel.Message = ex.Message;
                }
            });
            action.BeginInvoke(new AsyncCallback((rlt) =>
            {
                if (rlt.IsCompleted)
                {
                    CanLogin = true;
                        //if (string.IsNullOrEmpty(rltModel.Message))
                        //    rltModel.Message = Consts.ReturnCodeMessage.GetMessage(result);
                        if (result != null)
                        rltModel.IsSuccess = true;
                    else
                        rltModel.IsSuccess = false;
                    if (GetResult != null)
                        GetResult(rltModel);
                }
            }), null);
        }
        #endregion

        #region 选择登录角色
        //void ExecSelectedIndexChanged(object parameter)
        //{
        //    DropdownItem item = parameter as DropdownItem;
        //    if (item.ID == 0)
        //    {
        //        SelectRoleCanLogin = false;
        //    }
        //    else
        //    {
        //        SelectRoleCanLogin = true;
        //    }
        //}
        #endregion
        #endregion

    }
}
