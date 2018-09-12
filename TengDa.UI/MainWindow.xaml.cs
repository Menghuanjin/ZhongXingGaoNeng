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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TengDa.Model;
using TengDa.Core;
using TengDa.DB;
using System.Collections.ObjectModel;
using TengDa.Models;
using System.Threading;
using TengDa.UI.Views;
using System.Windows.Threading;
using TengDa.UI.MessageBox;
using TengDa.UI.MESManager;
using TengDa.UI.MonitorView;
using System.Diagnostics;

namespace TengDa.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<PageItem> ItemPageData { get; set; }
        TreeViewItem selectItem = null;
        TengDa.DB.MesConfigureDB mesDB = new DB.MesConfigureDB();
        private DispatcherTimer refreshTimer = new DispatcherTimer();

        public void RefreshData()
        {
            this.refreshTimer.Interval = new TimeSpan(0, 0, 1);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();
          TengDa.Communication.DataGather.loadingBasicInfo();
            AppContext.Current.MesModel = mesDB.GetUserRolesById();
        }
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            SysTime.Text=string.Format("当前时间：{0}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        public string UserName
        {
            get { return AppContext.Current.UserInfo.UserName; }
        }

        public string RoleName { get; set; }

        
        public MainWindow()
        {
            InitializeComponent();     
            btnExit.Click += BtnExit_Click;
            btnMinimum.Click += BtnMinimum_Click;
            btnSwitchUser.Click += BtnSwitchUser_Click;
            LoadMenus();
            ItemPageData = new ObservableCollection<PageItem>();
            pageItem.ItemsSource = ItemPageData;
            lvwMenu.ItemsSource = ItemPageData;
            RefreshData();
            //Thread baking = new Thread(new ThreadStart(OMRON_PLC.DataGather.BeginAssignment));
            //baking.IsBackground = true;
            //baking.Start();
        }
        private void BtnSwitchUser_Click(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBoxResult.Yes == MessageBox.CustomMessageBox.Show("是否重新登录？", MessageBox.CustomMessageBoxButton.YesNo, MessageBox.CustomMessageBoxIcon.Question))
            {
                Login loginWindow = new Login();
                loginWindow.Show();
                Application.Current.MainWindow = loginWindow;
                this.Close();

            }
        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnSwitchUser.Content = new BindButton() { Content = "重新登录", ImageSource = "/TengDa.UI;component/image/32/login_32.png" };
            btnMinimum.Content = new BindButton() { Content = "最小化", ImageSource = "/TengDa.UI;component/image/32/browser-minimise-2.png" };
            btnExit.Content = new BindButton() { Content = "退出", ImageSource = "/TengDa.UI;component/image/32/Close_32.png" };
            AppContext.Current.RoleData = new System.Collections.ObjectModel.ObservableCollection<Models.Entities.RolesEntity>( (new RolesDB()).GetAll());
            RoleName = AppContext.Current.RoleData.FirstOrDefault((p) => p.RoleId == AppContext.Current.UserInfo.RoleId).RoleName;
            this.DataContext = this;        
        }

        #region 顶部按钮
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBoxResult.Yes == MessageBox.CustomMessageBox.Show("是否退出当前程序？", MessageBox.CustomMessageBoxButton.YesNo, MessageBox.CustomMessageBoxIcon.Question))
            {
                Process.GetCurrentProcess().Kill();
            }

        }
        private void BtnMinimum_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region 加载菜单栏

        private List<MenuNode> mainNodes = new List<MenuNode>();
        private PermissionNode lastSelectdChildNode;
        private void LoadMenus()
        {
            //this.BuildMenus(AppContext.Current.GetPermissionNodesAsync());
            tvwMenu.ItemsSource = AppContext.Current.GetPermissionNodesAsync();
        }

        void NavigatePage(string pageTitle, Page page)
        {
            if (ItemPageData.Count > 0)
            {
                PageItem data = ItemPageData.FirstOrDefault((p) => p.PageTitle.Equals(pageTitle));
                if (data != null)
                {
                    CheckedPageItem(data);
                    return;
                }
                for (int i = 0; i < ItemPageData.Count; i++)
                {
                    ItemPageData[i].CRNum = "0,0,0,0";
                    ItemPageData[i].IsChecked = false;
                    ItemPageData[i].BackColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF207BBC"));
                }
            }
            PageItem item = new PageItem();
            item.CRNum = "20,0,0,20";
            item.PageObj = page;
            item.PageTitle = pageTitle;
            item.IsChecked = true;
            item.ItemWidth = 25 + pageTitle.Length * 16;
            item.BackColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF49A9FF"));
            ItemPageData.Insert(0,item);
            mainPage.Navigate(page);

            bdrMenu.Visibility = Visibility.Visible;
            bgImg.Visibility = Visibility.Visible;
            VisualTreeHelper.GetOffset(btnExit);
            VisualTreeHelper.GetOffset(btnMinimum);
        }
        #endregion

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            selectItem.IsSelected = false;
            PageItem itemInfo = ItemPageData.FirstOrDefault((p) => p.IsChecked);
            itemInfo.IsChecked = false;
            Button btn = sender as Button;
            PageItem pageInfo = btn.Tag as PageItem;
            if (pageInfo != null)
            {
                int index = ItemPageData.IndexOf(pageInfo);
                if (index == 0 && ItemPageData.Count > 1)
                {
                    ItemPageData[1].CRNum = "20,0,0,20";
                    if (pageInfo.IsChecked)
                    {
                        ItemPageData[1].IsChecked = true;
                        ItemPageData[1].BackColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF49A9FF"));
                    }
                }
                ItemPageData.Remove(pageInfo);
            }
            if (pageInfo != itemInfo)
            {
                System.Threading.ThreadPool.QueueUserWorkItem((p)=> { Thread.Sleep(100);
                    this.Dispatcher.Invoke(new Action(() => { itemInfo.IsChecked = true; }));
                });                
            }
            else if (ItemPageData.Count > 0)
            {
                //ItemPageData[0].IsChecked = true;
                //ItemPageData[0].BackColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF49A9FF"));
                CheckedPageItem(ItemPageData[0]);
            }

            if (ItemPageData.Count == 0)
            {
                mainPage.Content = null;
                bdrMenu.Visibility = Visibility.Hidden;
                bgImg.Visibility = Visibility.Hidden;
            }
        }

        private void txtPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;
            PageItem pageInfo = txt.Tag as PageItem;
            CheckedPageItem(pageInfo);

        }

        private void CheckedPageItem(PageItem pageInfo)
        {
            for (int i = 0; i < ItemPageData.Count; i++)
            {
                ItemPageData[i].IsChecked = false;
                ItemPageData[i].BackColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF207BBC"));
            }
            if (pageInfo != null)
            {
                mainPage.Navigate(pageInfo.PageObj);
                pageInfo.IsChecked = true;
                pageInfo.BackColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF49A9FF"));
            }
        }

        private void imgMore_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popusBottom.IsOpen = true;   
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is Image))// || e.OriginalSource is Border))
            {
                popusBottom.IsOpen = false;
            }
     

        }
        

        private void lvwMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvwMenu.SelectedItem != null)
            {
                PageItem item = lvwMenu.SelectedItem as PageItem;
                if (item != null)
                {
                    CheckedPageItem(item);
                    popusBottom.IsOpen = false;
                }
            }
        }

        private void tvwMenu_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PermissionNode tag = (PermissionNode)tvwMenu.SelectedItem;
            if (tag == null)
            {
                return;
            }
            if ((tag != this.lastSelectdChildNode) && (this.lastSelectdChildNode != null))
            {
                //this.lastSelectdChildNode.Button.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
            this.lastSelectdChildNode = tag;
            //tag.Button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/TengDa.UI;component/image/menu_child_button_bg.png", UriKind.Absolute)));//state_abnormal.png

            switch (tag.Code)
            {
                case "Situation.Standby":
                    MainMonitorPage MonitorPage = new MainMonitorPage();
                    NavigatePage(tag.Text, MonitorPage);
                    break;
                //case "Situation.Standby":
                //    this.mainPage.Navigate(new Uri("BakingPage.xaml", UriKind.Relative));
                //    break;
                case "UserManager.Personal":
                    PersonalInfoPage page = new PersonalInfoPage();
                    NavigatePage(tag.Text, page);
                    //this.mainPage.Navigate(new Uri("Views/PersonalInfoPage.xaml", UriKind.Relative));
                    break;
                case "UserManager.User":
                    UserManagerPage userPage = new UserManagerPage();
                    NavigatePage(tag.Text, userPage);
                    //this.mainPage.Navigate(new Uri("Views/UserManagerPage.xaml", UriKind.Relative));
                    break;
                case "UserManager.Role":
                    RoleManagerPage RolePage = new RoleManagerPage();
                    NavigatePage(tag.Text, RolePage);
                    break;
                case "ChuckingManager.ChuckingInfoPage":
                    ChuckingInfoPage ChuckingPage = new ChuckingInfoPage();
                    NavigatePage(tag.Text, ChuckingPage);
                    break;
                case "MESManager.MESInfoPage":
                    MESInfoPage MESPage = new MESInfoPage();
                    NavigatePage(tag.Text, MESPage);
                    break;
                case "MonitorView.TemperaturePage":
                   TemperaturePage Tempe = new TemperaturePage();
                    NavigatePage(tag.Text, Tempe);
                    break;
                case "MonitorView.RunLogPage":
                    RunLogPage  log = new RunLogPage();
                    NavigatePage(tag.Text, log);
                    break;

                case "MonitorView.HistoryTemPage":
                    HistoryTemPage htp = new HistoryTemPage();
                    NavigatePage(tag.Text, htp);
                    break;
                case "MonitorView.HistoryErrorPage":
                    HistoryErrorPage hep = new HistoryErrorPage();
                    NavigatePage(tag.Text, hep);
                    break;
                case "MonitorView.MANUDebugging":
                    MANUDebuggingPage manud = new MANUDebuggingPage();
                    NavigatePage(tag.Text, manud);
                    break;

            }
            popusBottom.IsOpen = false;    
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePasswordDialog().ShowDialog();          
        }

        private void tvwMenu_LostFocus(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item  != null)
            {
                selectItem = item;
            }
        }




    }

    public class PageItem:BaseModel
    {
        public string PageTitle { get; set; }

        public Page PageObj { get; set; }

        private string _CRNum;

        public string CRNum
        {
            get { return _CRNum; }
            set { _CRNum = value;this.RaisePropertyChanged("CRNum"); }
        }


        private bool _IsChecked;

        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value;this.RaisePropertyChanged("IsChecked"); }
        }

        private int _ItemWidth = 89;//4个字菜单为基准，加减一个字+(-)16

        public int ItemWidth
        {
            get { return _ItemWidth ; }
            set { _ItemWidth = value;this.RaisePropertyChanged("ItemWidth"); }
        }



        private SolidColorBrush _BackColor= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF49A9FF"));
        public SolidColorBrush BackColor
        {
            get { return _BackColor ; }
            set { _BackColor = value; this.RaisePropertyChanged("BackColor"); }
        }

    }
}
