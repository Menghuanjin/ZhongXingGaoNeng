using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TengDa.UserControls.UDateTime
{
    [System.ComponentModel.DesignTimeVisible(false)]//在工具箱中 隐藏该窗体 20170804 姜彦
                                                    /// <summary>
                                                    /// TDateTime.xaml 的交互逻辑
                                                    /// </summary>
    public partial class TDateTimeView : UserControl
    {
        public TDateTimeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public TDateTimeView(string txt)
            : this()
        {
            this.formerDateTimeStr = txt;
            //textBlockhh.PreviewMouseDown += new MouseButtonEventHandler(textBlockhh_PreviewMouseDown);//注意，这个事件的注册必须在textBlockhh获得焦点之前
            //textBlockhh.GotFocus += new RoutedEventHandler(textBlockhh_GotFocus);
            //textBlockhh.LostFocus += new RoutedEventHandler(textBlockhh_LostFocus);
        }

        void textBlockhh_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.PreviewMouseDown += new MouseButtonEventHandler(textBlockhh_PreviewMouseDown);
        }

        void textBlockhh_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Focus();
            e.Handled = true;
        }

        void textBlockhh_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.SelectAll();
            tb.PreviewMouseDown -= new MouseButtonEventHandler(textBlockhh_PreviewMouseDown);
        }

        #region 全局变量

        /// <summary>
        /// 从 DateTimePicker 传入的日期时间字符串
        /// </summary>
        private string formerDateTimeStr = string.Empty;

        // private string selectDate = string.Empty;    

       
        #endregion   

        #region 事件

        /// <summary>
        /// TDateTimeView 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //当前时间
            DateTime dt = Convert.ToDateTime(this.formerDateTimeStr);
            textBlockhh.Text = dt.Hour.ToString().PadLeft(2, '0');
            textBlockmm.Text = dt.Minute.ToString().PadLeft(2, '0');
            textBlockss.Text = dt.Second.ToString().PadLeft(2, '0');

            //00:00:00            
            //textBlockhh.Text = "00";
            //textBlockmm.Text = "00";
            //textBlockss.Text = "00";
        }


        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iBtnCloseView_Click(object sender, RoutedEventArgs e)
        {
            OnDateTimeContent(this.formerDateTimeStr);
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dt = new DateTime?();

            if (calDate.SelectedDate == null)
            {
                dt = DateTime.Now.Date;
            }
            else
            {
                dt = calDate.SelectedDate;
            }

            DateTime dtCal = Convert.ToDateTime(dt);

            string timeStr = "00:00:00";
            timeStr = textBlockhh.Text + ":" + textBlockmm.Text + ":" + textBlockss.Text;

            string dateStr;
            dateStr = dtCal.ToString("yyyy/MM/dd");

            string dateTimeStr;
            dateTimeStr = dateStr + " " + timeStr;

            string str1 = string.Empty; ;
            str1 = dateTimeStr;
            OnDateTimeContent(str1);

        }

        /// <summary>
        /// 当前按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNow_Click(object sender, RoutedEventArgs e)
        {
            popChioce.IsOpen = false;//THourView 或 TMinSexView 所在pop 的关闭动作

            if (btnNow.Content.ToString() == "零点")
            {
                textBlockhh.Text = "00";
                textBlockmm.Text = "00";
                textBlockss.Text = "00";
                btnNow.Content = "当前";
                btnNow.Background = System.Windows.Media.Brushes.LightBlue;
            }
            else
            {
                DateTime dt = DateTime.Now;
                textBlockhh.Text = dt.Hour.ToString().PadLeft(2, '0');
                textBlockmm.Text = dt.Minute.ToString().PadLeft(2, '0');
                textBlockss.Text = dt.Second.ToString().PadLeft(2, '0');
                btnNow.Content = "零点";
                btnNow.Background = System.Windows.Media.Brushes.LightGreen;
            }


        }
        

        /// <summary>
        /// 解除calendar点击后 影响其他按钮首次点击无效的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calDate_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);
            }
        }


        #endregion

        #region Action交互

        /// <summary>
        /// 时间确定后的传递事件
        /// </summary>
        public Action<string> DateTimeOK;

        /// <summary>
        /// 时间确定后传递的时间内容
        /// </summary>
        /// <param name="dateTimeStr"></param>
        protected void OnDateTimeContent(string dateTimeStr)
        {
            if (DateTimeOK != null)
                DateTimeOK(dateTimeStr);
        }

        #endregion
        
    }
}
