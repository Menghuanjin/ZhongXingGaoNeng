
using System;
using System.Collections.Generic;
using System.Data;
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
using TengDa.Common;
using TengDa.Core;
using TengDa.DB;
using TengDa.Model;
using TengDa.Model.Entities;
using TengDa.Models;
using TengDa.Models.Entities;
using TengDa.Models.ViewModel;
using TengDa.UI.ChuckingManager;
using TengDa.UI.MessageBox;
using TengDa.UI.Views;
using TengDa.UserControls;

namespace TengDa.UI
{
    /// <summary>
    /// PersonalInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChuckingInfoPage : BasePage, IComponentConnector
    {
        public ChuckingInfoPage()
        {
            InitializeComponent();
            RefreshData();

        }
        //DetailDB Detail = new DetailDB();


        int FFMID = 0;
        public void RefreshData()
        {

            IEnumerable<FixtureFurnaceMainEntity> FixtureFurnaceMainList = (new FixtureFurnaceMainDB()).GetAllData();
            this.MainDataGrid.ItemsSource = FixtureFurnaceMainList;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            bool? nullable = new ChuckingDialog().ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FButton_Click_1(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
        /// <summary>
        /// 修改主表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviseMain_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FixtureFurnaceMainEntity main = (FixtureFurnaceMainEntity)btn.Tag;// as FixtureFurnaceMain;
            ChuckingDialog view = new ChuckingDialog(main);
            view.Model = JSONHelper.Deserialize<FixtureFurnaceMainEntity>(JSONHelper.Serialize(main));
            bool? nullable = view.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            RefreshData();
        }
        /// <summary>
        /// 删除主表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMain_Click(object sender, RoutedEventArgs e)
        {

            if (CustomMessageBoxResult.Yes == MessageBox.CustomMessageBox.Show("是否删除当前主机台数据？", MessageBox.CustomMessageBoxButton.YesNo, MessageBox.CustomMessageBoxIcon.Question))
            {
                Button btn = sender as Button;
                FixtureFurnaceMainEntity fixtureFurnaceMainEntity = btn.Tag as FixtureFurnaceMainEntity;
                if ((new FixtureFurnaceMainDB()).Delete(fixtureFurnaceMainEntity.FFMId) < 1)
                {
                    CustomMessageBox.Show("删除数据过程出现错误！", CustomMessageBoxButton.OK, CustomMessageBoxIcon.Question);

                    return;
                }
                RefreshData();
            }
            //}
        }
        /// <summary>
        /// 根据主表查找相关的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FixtureFurnaceMainEntity fixtureFurnaceMain = MainDataGrid.SelectedItem as FixtureFurnaceMainEntity;
            if (fixtureFurnaceMain != null)
                FFMID = fixtureFurnaceMain.FFMId;
            if (fixtureFurnaceMain != null)
            {
                List<FixtureFurnaceDetaiViewModel> FixtureFurnaceDetailList = (new FixtureFurnaceDetailDB()).GetAllData().Where(x => x.FFMId == FFMID).ToList();
                this.DetailDataGrid.ItemsSource = FixtureFurnaceDetailList;
            }
        }
        /// <summary>
        /// 添加明细表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FixtureFurnaceMainEntity main = (FixtureFurnaceMainEntity)btn.Tag;// as FixtureFurnaceMain;
            FixtureFurnaceDetaiViewModel vmModel = new FixtureFurnaceDetaiViewModel();

            ObjectReflection.AutoMapping(main, vmModel);
            bool? nullable = new ChuckingDetailDialog(vmModel, 1).ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            this.RefreshData();
        }
        /// <summary>
        /// 明细修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviseDetail_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            FixtureFurnaceDetaiViewModel Detail = (FixtureFurnaceDetaiViewModel)btn.Tag;// as FixtureFurnaceMain;
            ChuckingDetailDialog view = new ChuckingDetailDialog(Detail,0);
        
            bool? nullable = view.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag ? (nullable.HasValue ? 1 : 0) : 0) == 0)
                return;
            //RefreshData();
            HostDataGrid_SelectionChanged(null, null);
        }
        private void DeleteDetail_Click(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBoxResult.Yes == MessageBox.CustomMessageBox.Show("是否删除当前机台明细信息？", MessageBox.CustomMessageBoxButton.YesNo, MessageBox.CustomMessageBoxIcon.Question))
            {
                FixtureFurnaceDetaiViewModel fixtureFurnaceDetaiViewModel = DetailDataGrid.SelectedItem as FixtureFurnaceDetaiViewModel;

                if (fixtureFurnaceDetaiViewModel != null)
                {
                    if ((new FixtureFurnaceDetailDB()).Delete(fixtureFurnaceDetaiViewModel.FFDId) < 1)
                    {
                        CustomMessageBox.Show("删除数据过程出现错误！", CustomMessageBoxButton.OKCancel, CustomMessageBoxIcon.Question);
                        return;
                    }
                    HostDataGrid_SelectionChanged(null, null);
                }
            }
        }

    }
}
