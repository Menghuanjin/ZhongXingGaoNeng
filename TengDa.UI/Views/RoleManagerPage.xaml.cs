
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
using TengDa.DB;
using TengDa.Models.Entities;
using TengDa.UI.Views;
using TengDa.UserControls;

namespace TengDa.UI
{
    /// <summary>
    /// RoleManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class RoleManagerPage : BasePage, IComponentConnector
    {
        public RoleManagerPage()
        {
            InitializeComponent();
            this.pager.PageNumberChanged += new EventHandler<PageNumberChangedEventArgs>(this.Pager_PageNumberChanged);
            this.DataContext = this;
        }

       

        private void Pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
            this.RefreshData();
        }

        public void RefreshData()
        {
            AppContext.Current.RoleData = new System.Collections.ObjectModel.ObservableCollection<Models.Entities.RolesEntity>((new RolesDB()).GetAll());
            this.dataGrid.ItemsSource = AppContext.Current.RoleData;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new RoleAddDialog().ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }
        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            RolesEntity role = btn.Tag as RolesEntity;
            //if (MessageBox.Show("确认删除该角色吗？", "提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            //    return;
            if ((new RolesDB()).Delete(role) < 1)
            {
                //MessageBox.Show("删除数据过程出现错误！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                AppContext.Current.RoleData.Remove(role);
            }
        }
        /// <summary>
        /// 账户修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            RolesEntity role = btn.Tag as RolesEntity;
            RoleAddDialog view = new RoleAddDialog(role);
            bool? nullable = view.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            RefreshData();
        }

    }
}