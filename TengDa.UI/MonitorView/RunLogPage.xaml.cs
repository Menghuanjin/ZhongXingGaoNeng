using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TengDa.UserControls;

namespace TengDa.UI.MonitorView
{
    /// <summary>
    /// RunLogPage.xaml 的交互逻辑
    /// </summary>
    public partial class RunLogPage : BasePage
    {
        public RunLogPage()
        {
            InitializeComponent();
           this.RefreshData();

        }
        private DispatcherTimer refreshTimer = new DispatcherTimer();
        public void RefreshData()
        {
            this.refreshTimer.Interval = new TimeSpan(0, 0, 5);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();
        }
        /// <summary>
        /// 日志文本框滚动条是否在最下方
        /// true:文本框竖直滚动条在文本框最下面时，可以在文本框后追加日志
        /// false:当用户拖动文本框竖直滚动条，使其不在最下面时，即用户在查看旧日志，此时不添加新日志，
        /// </summary>
        public bool IsVerticalScrollBarAtBottom
        {
            get
            {
                bool atBottom = false;

                this.txtLog.Dispatcher.Invoke((Action)delegate
                {
                    //if (this.txtLog.VerticalScrollBarVisibility != ScrollBarVisibility.Visible)
                    //{
                    //    atBottom= true;
                    //    return;
                    //}
                    double dVer = this.txtLog.VerticalOffset;       //获取竖直滚动条滚动位置
                    double dViewport = this.txtLog.ViewportHeight;  //获取竖直可滚动内容高度
                    double dExtent = this.txtLog.ExtentHeight;      //获取可视区域的高度

                    if (dVer + dViewport >= dExtent)
                    {
                        atBottom = true;
                    }
                    else
                    {
                        atBottom = false;
                    }
                });

                return atBottom;
            }
        }

        public void RefreshTimer_Tick(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                txtLog.Clear();
                txtLog.AppendText( TengDa.Common.logHelp .LocalLog);
                //自动滚动到底部
                //txtLog.ScrollToHome();

                //大于100行清除记录，可选
                if (txtLog.LineCount > 70)
                {
                    //txtLog.Clear();
                    //string[] lines = I_Factory.Commom.SysInfo.LocalLog.Split('\n');
                    TengDa.Common.logHelp.LocalLog = TengDa.Common.logHelp.LocalLog.Substring(0, TengDa.Common.logHelp.LocalLog.Length / 2);
                }

                txtTransport.Clear();
                txtTransport.AppendText(TengDa.Common.logHelp.TransportLog);
                //自动滚动到底部
                //txtLog.ScrollToHome();

                ////大于100行清除记录，可选
                //if (txtLog.LineCount > 70)
                //{
                //    //txtLog.Clear();
                //    //string[] lines = I_Factory.Commom.SysInfo.LocalLog.Split('\n');
                //    TengDa.Common.logHelp.TransportLog = TengDa.Common.logHelp.TransportLog.Substring(0, TengDa.Common.logHelp.TransportLog.Length / 2);
            //    }
            }));

        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshTimer_Tick(null, null);
        }

        private void BasePage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.refreshTimer.Stop();
        }
    }
}
