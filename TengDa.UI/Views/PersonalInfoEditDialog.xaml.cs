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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TengDa.Common;
using TengDa.Core;
using TengDa.DB;
using TengDa.Model.Entities;
using Util.Controls;

namespace TengDa.UI.Views
{
    /// <summary>
    /// PersonalInfoEditDialog.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInfoEditDialog : WindowBase, IComponentConnector
    {
        public UsersEntity userInfo { get; set; }
        public PersonalInfoEditDialog(UsersEntity user)
        {
            InitializeComponent();
            userInfo = JSONHelper.Deserialize<UsersEntity>(JSONHelper.Serialize(user));
            this.DataContext = userInfo;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            UsersEntity user = this.DataContext as UsersEntity;
            if (string.IsNullOrEmpty(user.UserName))
            {
                int num = (int)MessageBox.CustomMessageBox.Show("请输入姓名!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question); //MessageBox.Show("请输入姓名", "提示");
            }
            else
            {
                if ((new UsersDB()).Update(user))
                {
                    AppContext.Current.UserInfo = user;
                    this.DialogResult = new bool?(true);
                    this.Close();
                }
                else
                {
                    MessageBox.CustomMessageBox.Show("修改过程出现错误!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question); //MessageBox.Show("修改过程出现错误！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void WindowBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition(this))
    || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }
        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            this.ddlGender.ItemsSource = new List<string> { "男", "女" };
        }


    }
}
