
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TengDa.Core;
using TengDa.Model;
using TengDa.Model.Entities;
using TengDa.UI.Views;
using TengDa.UserControls;

namespace TengDa.UI
{
    /// <summary>
    /// PersonalInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInfoPage : BasePage, IComponentConnector
    {
        public PersonalInfoPage()
        {
            InitializeComponent();
        }
        //public User model;

        public void RefreshData()
        {
            //UserGetResponse userGetResponse = BLL.User.UserManager.GetUser(new UserGetRequest() { UserId = AppContext.Current.UserId });
            //if (userGetResponse.User == null)
            //    return;
            this.dataGrid.ItemsSource = new List<UsersEntity> { AppContext.Current.UserInfo };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void btnEditInfo_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new PersonalInfoEditDialog(AppContext.Current.UserInfo).ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordDialog().ShowDialog();
        }
    }
}
