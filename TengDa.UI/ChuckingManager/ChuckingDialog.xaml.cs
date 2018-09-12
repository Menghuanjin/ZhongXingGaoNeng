using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
using TengDa.Models;
using TengDa.Models.Common;
using TengDa.Models.Entities;
using Util.Controls;

namespace TengDa.UI.Views
{
    /// <summary>
    /// RoleAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChuckingDialog : WindowBase, IComponentConnector
    {
        public ChuckingDialog()
        {
            InitializeComponent();

        }
        public FixtureFurnaceMainEntity Model { get; set; }
        public ChuckingDialog(FixtureFurnaceMainEntity role)
        {
            InitializeComponent();
        }
        
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.FFMIsAvailable = Convert.ToInt32(Enum.Parse(typeof(YesOrNoType), Available.SelectedValue.ToString()));
                Model.FFMBerthPosition = (int)Enum.Parse(typeof(BerthPositionType), Position.SelectedValue.ToString());
                Model.FFMState = (int)Enum.Parse(typeof(PLCStateType), cmbFFMState.SelectedValue.ToString());
                Model.FFMCreateType= (int)Enum.Parse(typeof(CreateControlType), cmbCreateType.SelectedValue.ToString());

                if (this.Title.Contains("添加"))
                {
                    Model.CreateTime = DateTime.Now;
                    Model.CreateUser = TengDa.Core.AppContext.Current.UserInfo.UserName;
                    if ((new FixtureFurnaceMainDB()).Insert(Model) < 1)
                    {
                        MessageBox.CustomMessageBox.Show("添加数据过程出现错误!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                        return;
                    }
                }
                else
                {
                    if (!(new FixtureFurnaceMainDB()).Update(Model))
                    {
                        MessageBox.CustomMessageBox.Show("修改数据过程出现错误！", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                        return;
                    }
                }
                this.DialogResult = new bool?(true);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBoxX.Error(ex.Message);
            }

        }
        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {        
            this.Available.ItemsSource = YesOrNoType.Yes.ToArrayList();
            this.Position.ItemsSource = BerthPositionType.Top.ToArrayList();
            this.cmbFFMState.ItemsSource = PLCStateType.Working.ToArrayList();
            this.cmbCreateType.ItemsSource = CreateControlType.Stove.ToArrayList();

            if (this.Model != null)
            {
                YesOrNoType yesOrNo = (YesOrNoType)Convert.ToInt32(Model.FFMIsAvailable);
                Available.Text = yesOrNo.FetchDescription();

                BerthPositionType BerthPosition = (BerthPositionType)Convert.ToInt32(Model.FFMBerthPosition);
                Position.Text = BerthPosition.FetchDescription();

                PLCStateType plcState = (PLCStateType)Convert.ToInt32(Model.FFMState);
                cmbFFMState.Text = plcState.FetchDescription();

                CreateControlType createType = (CreateControlType)Convert.ToInt32(Model.FFMCreateType);
                cmbCreateType.Text = createType.FetchDescription();

                this.Title = "修改信息";
            }
            else
            {
                this.Title = "添加信息";
                this.Model = new FixtureFurnaceMainEntity();
            }
            this.DataContext = this.Model;
        }
        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }
    

    }
}
