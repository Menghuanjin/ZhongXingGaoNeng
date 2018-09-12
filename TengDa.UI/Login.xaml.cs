
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Windows.Shapes;
using TengDa.DB;
using TengDa.Model;
using TengDa.UI.MessageBox;
using TengDa.ViewModel.VW;

namespace TengDa.UI
{

    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        LoginViewModel vm = null;
        public Login()
        {
          //  TengDa.Common.CSVUtil.SaveInfoToCSVFileAtLog( DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss:ff"), "在线", "noteEquipmentStatus", "test", "成功", "无");

            InitializeComponent();

        


            // TengDa.Common.PLCAddress.ConnectPLC( "192.168.250.50", 9600);
            TengDa.Common.HtmlHelp.LoadData(AppDomain.CurrentDomain.BaseDirectory + @"\\doc\\demo.html");//加载Html文件
            txtUserName.Focus();//聚焦在用户名输入框中
            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/Background.png"));
            //b.ImageSource = new BitmapImage(new Uri("/TengDa.UI;component/image/Background.png"));
            b.Stretch = Stretch.Fill;
            bgImg.Background = b;

            vm = new LoginViewModel();
            this.DataContext = vm;
            vm.GetResult += vm_GetResult;
            //this.btnLogin.Click += btnLogin_Click;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        void vm_GetResult(Result rlt)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (rlt.IsSuccess)
                {
                    try {
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                       TengDa.Common.Log.Logs.Info("登录成功：用户"+TengDa.Core.AppContext.Current.UserInfo.UserName);
                    }
                    catch(Exception ex)
                    {
                        TengDa.Common.Log.Logs.Info("登录失败：" + ex.ToString());
                    }
                }
                else
                {
                    TengDa.UI.MessageBox.CustomMessageBox.Show(rlt.Message, CustomMessageBoxButton.OK, CustomMessageBoxIcon.Error);
                }
            }));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
