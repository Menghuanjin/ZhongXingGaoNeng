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
using TengDa.Model;
using TengDa.Models;

namespace TengDa.UserControls
{
    /// <summary>
    /// OvenView.xaml 的交互逻辑1
    /// </summary>
    public partial class OvenView : Grid, IComponentConnector
    {
        OvenViewModel viewModel = new OvenViewModel();
        public delegate void DgBtnMethod(TempBtnInfoEx btnInfo);
        public event DgBtnMethod EBtnMethod;
        public OvenView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        /// <summary>
        /// 自动生成炉腔层数
        /// </summary>
        public void CreateKilnInfo(int num,string name,string number)
        {
            int stoveNum = 0;
            for (int i = 1; i <= num; i++)
            {
                TempBtnInfo btnInfo = new TempBtnInfo();
                TempBtnInfoEx aheadBtn = new TempBtnInfoEx();
                TempBtnInfoEx backBtn = new TempBtnInfoEx();
                viewModel.IndexName = name;
                viewModel.Index =Convert.ToInt32(number);
                aheadBtn.BtnIndex = stoveNum++;
                backBtn.BtnIndex = stoveNum++;
                //aheadBtn.TempNum = "19.8°";//string.Format(@"tb{0}And{1}", i, j);
                //backBtn.TempNum = "17.0°";//string.Format(@"tb{0}And{1}", i, j);
                aheadBtn.BtnCode = string.Empty;
                backBtn.BtnCode = string.Empty;
                if (i == 1)
                {
                    aheadBtn.BtnStyle = this.FindResource("MainButton") as Style;
                    backBtn.BtnStyle = this.FindResource("MainButton") as Style;
                    //aheadBtn.BtnStyle = this.FindResource("TopLeftButton") as Style;
                    //backBtn.BtnStyle = this.FindResource("TopRightButton") as Style;
                }
                else if (i < num)
                {
                    aheadBtn.BtnStyle = this.FindResource("MainButton") as Style;
                    backBtn.BtnStyle = this.FindResource("MainButton") as Style;
                }
                else
                {
                    aheadBtn.BtnStyle = this.FindResource("BottomLeftButton") as Style;
                    backBtn.BtnStyle = this.FindResource("BottomRightButton") as Style;
                }
                btnInfo.AheadBtnInfo = aheadBtn;
                btnInfo.BackBtnInfo = backBtn;
                viewModel.TempBtnData.Add(btnInfo);
            }
        }
        /// <summary>
        /// 按下真空炉子按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TempBtnInfoEx code = (sender as Button).Tag as TempBtnInfoEx;
            if (EBtnMethod != null)
            {
                EBtnMethod(code);
            }
            //if (index == 1)
            //{
            //    viewModel.ImgSource = "image/greenlight.png";
            //}
            //else if (index == 2)
            //{
            //    viewModel.ImgSource = "image/redlight.png";
            //}
            //else
            //{
            //    viewModel.ImgSource = "image/yellowlight.png";
            //}
        }
    }
    public class OvenViewModel : BaseModel
    {   

        public int Index { get; set; }

        public string IndexName { get; set; }
        /// <summary>
        /// 图片提示信息
        /// </summary>
        private string _TipInfo;

        public string TipInfo
        {
            get { return _TipInfo; }
            set { _TipInfo = value;this.RaisePropertyChanged("TipInfo"); }
        }


        private string _ImgSource = "image/greenlight.png";

        public string ImgSource
        {
            get { return _ImgSource; }
            set { _ImgSource = value; this.RaisePropertyChanged("ImgSource"); }
        }


        private List<TempBtnInfo> _TempBtnData = new List<TempBtnInfo>();

        public List<TempBtnInfo> TempBtnData
        {
            get { return _TempBtnData; }
            set { _TempBtnData = value; }
        }

    }

    /// <summary>
    /// 按钮数据，同时存放一排（前后两个按钮）
    /// </summary>
    public class TempBtnInfo : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public TempBtnInfoEx AheadBtnInfo { get; set; }
        public TempBtnInfoEx BackBtnInfo { get; set; }


        public int _ProgressValue;
        /// <summary>
        /// 进度值
        /// </summary>
        public int ProgressValue
        {
            get { return _ProgressValue; }
            set { _ProgressValue = value; this.RaisePropertyChanged("ProgressValue"); }
        }


        public string _ProgressTitel;
        /// <summary>
        /// 进度文本提示
        /// </summary>
        public string ProgressTitel
        {
            get { return _ProgressTitel; }
            set { _ProgressTitel = value; this.RaisePropertyChanged("ProgressTitel"); }
        }

        public string _ProgressText;
        /// <summary>
        /// 进度文本值
        /// </summary>
        public string ProgressText {
            get { return _ProgressText; }
            set { _ProgressText = value; this.RaisePropertyChanged("ProgressText"); }
        }

        public bool _IsShow;
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return _IsShow; }
            set { _IsShow = value; this.RaisePropertyChanged("IsShow"); }
        }


    }
    /// <summary>
    /// 每层炉子
    /// </summary>
    public class TempBtnInfoEx:BaseModel
    {
        public TempBtnInfoEx()
        {
            LinearGradientBrush brush = new LinearGradientBrush();

            GradientStop gs1 = new GradientStop();
            gs1.Offset = 0;
            gs1.Color = (Color)ColorConverter.ConvertFromString("#FF377FED");
            brush.GradientStops.Add(gs1);

            GradientStop gs2 = new GradientStop();
            gs2.Offset = 1;
            gs2.Color = (Color)ColorConverter.ConvertFromString("#FF074CC0");
            brush.GradientStops.Add(gs2);
            BtnColor = brush;
        }
        private Brush _BtnColor;// = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF49A9FF"));

        public Brush BtnColor
        {
            get { return _BtnColor; }
            set { _BtnColor = value;this.RaisePropertyChanged("BtnColor"); }
        }


        private double _OpacityNum = 1;
        /// <summary>
        /// 透明度
        /// </summary>
        public double OpacityNum
        {
            get { return _OpacityNum; }
            set { _OpacityNum = value;this.RaisePropertyChanged("OpacityNum"); }
        }

        public string _ToolTipText;
        /// <summary>
        /// 鼠标显示文本
        /// </summary>
        public string ToolTipText
        {
            get { return _ToolTipText; }
            set { _ToolTipText = value; this.RaisePropertyChanged("ToolTipText"); }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int BtnIndex { get; set; }
        /// <summary>
        /// 夹具条码
        /// </summary>
        public string BtnCode { get; set; }
        private string _TempNum;
        /// <summary>
        /// 温度
        /// </summary>
        public string TempNum
        {
            get { return _TempNum; }
            set { _TempNum = value; this.RaisePropertyChanged("TempNum"); }
        }

        private Style _BtnStyle;
        /// <summary>
        /// 样式
        /// </summary>
        public Style BtnStyle
        {
            get { return _BtnStyle; }
            set { _BtnStyle = value; this.RaisePropertyChanged("BtnStyle"); }
        }
    }
}
