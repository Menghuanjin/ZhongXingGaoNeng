
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TengDa.Common;
using TengDa.DB;
using TengDa.Model.Entities;
using TengDa.Models;
using TengDa.UI.MessageBox;
using TengDa.UI.Views;
using TengDa.UserControls;

namespace TengDa.UI
{
    /// <summary>
    /// UserManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagerPage : BasePage, IComponentConnector
    {
        public string CrData { get; set; }
        public UserManagerPage()
        {
            InitializeComponent();

            this.pager.PageNumberChanged += new EventHandler<PageNumberChangedEventArgs>(this.Pager_PageNumberChanged);
            CrData = "20,0,0,0";
            this.DataContext = this;
        }
        
        private void Pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }

        public void RefreshData()
        {
            //UserListResponse userListResponse = BLL.User.UserManager.GetUserList(new UserListRequest() { PageNumber = this.pager.PageNumber, PageSize = 10 });
            //this.pager.Setup(userListResponse.Users);
            //this.dataGrid.ItemsSource = userListResponse.Users;
            long pageCount = -1;
            IEnumerable< UsersEntity> userInfos = (new UsersDB()).GetPageList(this.pager.PageNumber,10,out pageCount);
            int index = pageCount% 10 > 0 ? 1 : 0;
            this.pager.Setup((IPagedData)new PagedData<object>()
            {
                PageNumber = this.pager.PageNumber,
                PageCount = (int)pageCount / 10 + index,
                PageSize = 10
            });
            this.dataGrid.ItemsSource = userInfos;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           this.RefreshData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new UserEditDialog().ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBoxResult.Yes == MessageBox.CustomMessageBox.Show("确认删除该用户？", MessageBox.CustomMessageBoxButton.YesNo, MessageBox.CustomMessageBoxIcon.Question))
            {
                Button btn = sender as Button;
                UsersEntity user = btn.Tag as UsersEntity;
                if ((new UsersDB()).Delete(user.UserId) < 1)
                {
                    MessageBox.CustomMessageBox.Show("删除数据过程出现错误!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            UsersEntity user = btn.Tag as UsersEntity;
            UserEditDialog view = new UserEditDialog();
            view.Model = JSONHelper.Deserialize<UsersEntity>(JSONHelper.Serialize(user));
            bool? nullable = view.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            RefreshData();
        }
    }
}
