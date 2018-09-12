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
    /// MachinePlatform.xaml 的交互逻辑
    /// </summary>
    public partial class MachinePlatform : Grid, IComponentConnector
    {
        MachinePlatformModel model = new MachinePlatformModel();
        public delegate void DgBtnMethod(BtnInfoEx btnInfo);
        public event DgBtnMethod EBtnInfo;
        public MachinePlatform()
        {
            InitializeComponent();
            DataContext = model;
        }

        public void CreateKilnInfo(int num, string name, string number)
        {
            int stoveNum = 0;
            for (int i = 1; i <= num; i++)
            {
                TempListBtnInfo btnInfo = new TempListBtnInfo();
                BtnInfoEx btn = new BtnInfoEx();
                model.IndexName = name;
                model.Index = Convert.ToInt32(number);
                btn.BtnIndex = stoveNum++;
                btn.BtnCode = string.Empty;
                btn.BtnStyle = this.FindResource("MainButton") as Style;
                btnInfo.MainBtnInfo = btn;
                model.TempBtnData.Add(btnInfo);
            }
        }
        /// <summary>
        /// 点击上料平台层数事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BtnInfoEx code = (sender as Button).Tag as BtnInfoEx;
            if (EBtnInfo != null)
            {
                EBtnInfo(code);
            }
        }
    }
    public class MachinePlatformModel : BaseModel
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
            set { _TipInfo = value; this.RaisePropertyChanged("TipInfo"); }
        }


        private string _ImgSource = "image/greenlight.png";

        public string ImgSource
        {
            get { return _ImgSource; }
            set { _ImgSource = value; this.RaisePropertyChanged("ImgSource"); }
        }


        private List<TempListBtnInfo> _TempBtnData = new List<TempListBtnInfo>();

        public List<TempListBtnInfo> TempBtnData
        {
            get { return _TempBtnData; }
            set { _TempBtnData = value; }
        }

    }
    public class TempListBtnInfo : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public BtnInfoEx MainBtnInfo { get; set; }
     
    }
    public class BtnInfoEx : BaseModel
    {
        public BtnInfoEx()
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
            set { _BtnColor = value; this.RaisePropertyChanged("BtnColor"); }
        }


        private double _OpacityNum = 1;
        /// <summary>
        /// 透明度
        /// </summary>
        public double OpacityNum
        {
            get { return _OpacityNum; }
            set { _OpacityNum = value; this.RaisePropertyChanged("OpacityNum"); }
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
