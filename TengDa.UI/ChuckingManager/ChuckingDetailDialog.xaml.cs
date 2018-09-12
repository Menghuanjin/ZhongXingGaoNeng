using System;
using System.Collections.Generic;
using System.Linq;
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
using TengDa.DB;
using TengDa.Models.Common;
using TengDa.Models.Entities;
using TengDa.Models.ViewModel;
using TengDa.UI.MessageBox;
using Util.Controls;

namespace TengDa.UI.ChuckingManager
{
    /// <summary>
    /// ChuckingDetailDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChuckingDetailDialog : WindowBase, IComponentConnector
    {
        public ChuckingDetailDialog()
        {
            InitializeComponent();
        }
        public ChuckingDetailDialog(FixtureFurnaceDetaiViewModel vm,int type)
        {
            InitializeComponent();   
                   
            Model = vm;
            operateType = type;
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        int operateType = 0;
   
        /// <summary>
        /// 
        /// </summary>
        public FixtureFurnaceDetaiViewModel Model { set; get; }
        /// <summary>
        /// 主表
        /// </summary>
        public static FixtureFurnaceMainEntity Main = new FixtureFurnaceMainEntity();
        /// <summary>
        /// 明细表
        /// </summary>
        public FixtureFurnaceDetailEntity DetailModel { get; set; }
       

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DetailModel = new FixtureFurnaceDetailEntity();
                ObjectReflection.AutoMapping(Model, DetailModel);
                //明细表
                DetailModel.FFDState = (int)Enum.Parse(typeof(FFDState), cmbFFDState.SelectedValue.ToString());
                DetailModel.FFDIsAccomplish = (int)Enum.Parse(typeof(YesOrNoType), Accomplish.SelectedValue.ToString());
                DetailModel.CAId = cmbCAI.SelectedValue==null ? 0 : (int)cmbCAI.SelectedValue;


           
                if (operateType==1)
                {

                    if ((new FixtureFurnaceDetailDB().Insert(DetailModel) < 1))
                    {
                        CustomMessageBox.Show("添加数据过程出现错误！", CustomMessageBoxButton.OKCancel, CustomMessageBoxIcon.Question);
                        return;
                    }
                }
                else
                {
                    if (!(new FixtureFurnaceDetailDB()).Update(DetailModel))
                    {
                        CustomMessageBox.Show("修改数据过程出现错误！", CustomMessageBoxButton.OKCancel, CustomMessageBoxIcon.Question);
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
            // IEnumerable<FixtureFurnaceMainEntity> Main = (new FixtureFurnaceMainDB()).GetAllData();


            this.Accomplish.ItemsSource = YesOrNoType.Yes.ToArrayList();
            this.cmbFFDState.ItemsSource = FFDState.NoFixtureOpen.ToArrayList();
            this.cmbCAI.ItemsSource = (new ChuckingApplianceInfoDB()).GetAll().ToList().OrderBy(x => x.CAId);

            if (operateType == 1)
            {
             
                this.Title = "添加信息";
            }
            else
            {
                YesOrNoType yesOrNo = (YesOrNoType)Convert.ToInt32(Model.FFDIsAccomplish);
                Accomplish.Text = yesOrNo.FetchDescription();


                FFDState FFDState = (FFDState)Convert.ToInt32(Model.FFDState);
                cmbFFDState.Text = FFDState.FetchDescription();


                this.Title = "修改信息";
               // this.Model = new FixtureFurnaceDetaiViewModel();
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
