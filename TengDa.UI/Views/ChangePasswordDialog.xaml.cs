using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TengDa.Core;
using TengDa.DB;
using Util.Controls;

namespace TengDa.UI.Views
{
    /// <summary>
    /// ChangePasswordDialog1.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePasswordDialog : WindowBase
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtOldPassword.Password))
            {
                MessageBox.CustomMessageBox.Show("请输入旧密码!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                //MessageBoxX.Info("请输入旧密码");
            }
            else if (string.IsNullOrEmpty(this.txtNewPassword.Password))
            {
                MessageBox.CustomMessageBox.Show("请输入新密码!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
            }
            else if (string.IsNullOrEmpty(this.txtNewPasswordConfirm.Password))
            {
                MessageBox.CustomMessageBox.Show("请输入新密码确认!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
            }
            else if (this.txtNewPassword.Password != this.txtNewPasswordConfirm.Password)
            {
                MessageBox.CustomMessageBox.Show("两次输入的密码不一致!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
            }
            else if (AppContext.Current.UserInfo.UserPwd != this.txtOldPassword.Password)
            {
                MessageBox.CustomMessageBox.Show("原始密码输入错误!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
            }
            else
            {
                //ChangePasswordResponse passwordResponse =  I_Factory.BLL.User.UserManager.ChangePassword(
                //    new ChangePasswordRequest() {
                //        UserId = AppContext.Current.UserId,
                //        OldPassword = this.txtOldPassword.Password,
                //        NewPassword = this.txtNewPassword.Password });
                AppContext.Current.UserInfo.UserPwd = txtNewPassword.Password;
                if (!(new UsersDB()).Update(AppContext.Current.UserInfo))
                {
                    MessageBox.CustomMessageBox.Show("修改过程出现错误!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Error);
                }
                else
                {
                    this.DialogResult = new bool?(true);
                    this.Close();
                }
            }
        }
    }   
}
