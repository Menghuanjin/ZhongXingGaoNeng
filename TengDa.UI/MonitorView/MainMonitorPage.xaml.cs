
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TengDa.Common;
using TengDa.DB;
using TengDa.Model.Entities;
using TengDa.Models;
using TengDa.Models.Entities;
using TengDa.UserControls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Windows.Threading;
using TengDa.UI.MessageBox;

namespace TengDa.UI
{
    /// <summary>
    /// UserManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainMonitorPage : BasePage, IComponentConnector
    {
        /// <summary>
        /// 机器人图片
        /// </summary>
        public Image ImgMachine { get; set; }

        public Image ImgMachineAlarm { get; set; }
        public MainMonitorPage()
        {
            InitializeComponent();
        }
  
        
        private DispatcherTimer refreshTimer = new DispatcherTimer();
        #region 公共变量
        FixtureFurnaceMainDB fixtureFurnaceMainDB = new FixtureFurnaceMainDB();
        FixtureFurnaceDetailDB fixtureFurnaceDetailDB = new FixtureFurnaceDetailDB();
        RobotStateInfoDB robotStateInfoDB = new RobotStateInfoDB();
            
        #endregion

        #region  页面布局
        /// <summary>
        /// 动态生成页面
        /// </summary>
        void CreateView()
        {
            var AllList = fixtureFurnaceMainDB.GetAllData();
            var topList = AllList.Where(a => a.FFMBerthPosition == 1).OrderBy(b=>b.FFMRowSort).ToList();//查询上面机台数量
            var bottomList = AllList.Where(a => a.FFMBerthPosition == 2).ToList();//查询下面机台数量

            int countNum = topList.Count() > bottomList.Count() ? topList.Count() : bottomList.Count();
            CreateGrid(countNum);
            //for (int i = 0; i < 3; i++)
            //{
            //    Image img = new Image();
            //    img.Source = new BitmapImage(new Uri("/TengDa.UserControls/assets/stop.png", UriKind.Relative));
            //    img.Width = 130;
            //   // img.Source = new Uri("pack://application:,,,/TengDa.UserControls;component/image/ToBottom.png", UriKind.Absolute);
            //    MainView.Children.Add(img); //添加到Grid控件
            //    img.SetValue(Grid.RowProperty, 0); //设置按钮所在Grid控件的行
            //    img.SetValue(Grid.ColumnProperty, 2); //设置按钮所在Grid控件的列
            //                                                                     //machinePlatform.MouseDown += OvenView_MouseDown;
            //}
            for (int i = 0; i < countNum; i++)
            {
                if (i + 1 <= topList.Count() && topList[i].FFMBerthPosition == 1)//生成顶部机台
                {
                    if (topList[i].FFMCreateType == 1)//生成炉子
                    {
                        TengDa.UserControls.OvenView ovenView = new OvenView();
                        ovenView.EBtnMethod += OvenView_EBtnMethod;
                        ovenView.Name = string.Format("button" + topList[i].FFMId);
                        ovenView.CreateKilnInfo(Convert.ToInt32(topList[i].FFMCountLayer), topList[i].FFMName, topList[i].FFMNumber.ToString());
                        ovenView.VerticalAlignment = VerticalAlignment.Center;
                        ovenView.Width = 130; //MainView.RowDefinitions[0].Height.Value;
                        MainView.Children.Add(ovenView); //添加到Grid控件
                        ovenView.SetValue(Grid.RowProperty, 0); //设置按钮所在Grid控件的行
                        ovenView.SetValue(Grid.ColumnProperty, (topList[i].FFMRowSort - 1) * 2); //设置按钮所在Grid控件的列
                        ovenView.MouseDown += OvenView_MouseDown;
                    }
                    else if (topList[i].FFMCreateType == 2)//生成上下料平台
                    {
                        TengDa.UserControls.MachinePlatform machinePlatform = new MachinePlatform();
                        machinePlatform.MouseDown += OvenView_MouseDown;
                        machinePlatform.EBtnInfo += MachinePlatform_EBtnMethod;
                        machinePlatform.Name = string.Format("button" + topList[i].FFMId);
                        machinePlatform.CreateKilnInfo(Convert.ToInt32(topList[i].FFMCountLayer), topList[i].FFMName, topList[i].FFMNumber.ToString());
                        machinePlatform.VerticalAlignment = VerticalAlignment.Center;
                        machinePlatform.Width = 130; //MainView.RowDefinitions[0].Height.Value;
                        MainView.Children.Add(machinePlatform); //添加到Grid控件
                        machinePlatform.SetValue(Grid.RowProperty, 0); //设置按钮所在Grid控件的行
                        machinePlatform.SetValue(Grid.ColumnProperty, (topList[i].FFMRowSort - 1) * 2); //设置按钮所在Grid控件的列
                        //machinePlatform.MouseDown += OvenView_MouseDown;
                    }
                }
                if (i + 1 <= bottomList.Count() && bottomList[i].FFMBerthPosition == 2)//生成顶部机台
                {
                    if (bottomList[i].FFMCreateType == 1)//生成炉子
                    {
                        TengDa.UserControls.OvenView ovenView = new OvenView();
                        ovenView.EBtnMethod += OvenView_EBtnMethod;
                        ovenView.Name = string.Format("button" + bottomList[i].FFMId);
                        ovenView.CreateKilnInfo(Convert.ToInt32(bottomList[i].FFMCountLayer), bottomList[i].FFMName, bottomList[i].FFMNumber);
                        ovenView.VerticalAlignment = VerticalAlignment.Center;
                        ovenView.Width = 130; //MainView.RowDefinitions[0].Height.Value;
                        MainView.Children.Add(ovenView); //添加到Grid控件
                        ovenView.SetValue(Grid.RowProperty, 2); //设置按钮所在Grid控件的行
                        ovenView.SetValue(Grid.ColumnProperty, (bottomList[i].FFMRowSort - 1) * 2); //设置按钮所在Grid控件的列
                        ovenView.MouseDown += OvenView_MouseDown;
                    }
                    else if (bottomList[i].FFMCreateType == 2)//生成上下料平台
                    {
                        TengDa.UserControls.MachinePlatform machinePlatform = new MachinePlatform();
                        machinePlatform.EBtnInfo += MachinePlatform_EBtnMethod;
                        machinePlatform.MouseDown += OvenView_MouseDown;
                        machinePlatform.Name = string.Format("button" + bottomList[i].FFMId);
                        machinePlatform.CreateKilnInfo(Convert.ToInt32(bottomList[i].FFMCountLayer), bottomList[i].FFMName, bottomList[i].FFMNumber.ToString());
                        machinePlatform.VerticalAlignment = VerticalAlignment.Center;
                        machinePlatform.Width = 130; //MainView.RowDefinitions[0].Height.Value;
                        MainView.Children.Add(machinePlatform); //添加到Grid控件
                        machinePlatform.SetValue(Grid.RowProperty, 2); //设置按钮所在Grid控件的行
                        machinePlatform.SetValue(Grid.ColumnProperty, (bottomList[i].FFMRowSort - 1) * 2 < 0 ? 1 : (bottomList[i].FFMRowSort - 1) * 2); //设置按钮所在Grid控件的列
                                                                                                                                                        //machinePlatform.MouseDown += OvenView_MouseDown;
                    }
                }
            }



            #region 机器人图片属性设置
            ImgMachine = new Image();
            MainView.Children.Add(ImgMachine); //添加到Grid控件

            ImgMachineAlarm = new Image();
            MainView.Children.Add(ImgMachineAlarm); //添加到Grid控件

            ImgMachineAlarm.Stretch = Stretch.None;
            ImgMachineAlarm.Margin = new Thickness(0, 0, 5, 0);
            ImgMachineAlarm.HorizontalAlignment = HorizontalAlignment.Right;
            ImgMachineAlarm.VerticalAlignment = VerticalAlignment.Top;
            ImgMachineAlarm.ToolTip = "测试数据";
            #endregion
        }

        /// <summary>
        /// 生成Grid网格
        /// </summary>
        /// <param name="countNum"></param>
        void CreateGrid(int countNum)
        {
            MainView.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(6d, GridUnitType.Star) });
            MainView.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(2.5d, GridUnitType.Star) });
            MainView.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(6d, GridUnitType.Star) });
            MainView.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.5d, GridUnitType.Star) });


            for (int i = 1; i <= (countNum * 2) - 1; i++)
            {
                // MainView.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5d, GridUnitType.Star) });
                ColumnDefinition cd0 = new ColumnDefinition();
                if (i % 2 == 0)//中间的分割层
                {
                    if (countNum <= 6)  //6
                    {
                        cd0.Width = new GridLength(0.15d, GridUnitType.Star);
                    }
                    else
                    {
                        cd0.Width = new GridLength(6);
                    }
                }
                else
                {
                    if (countNum <= 6)//6
                    {
                        cd0.Width = new GridLength(5d, GridUnitType.Star);
                    }
                    else
                    {
                        cd0.Width = new GridLength(141);                        
                    }
                }
                MainView.ColumnDefinitions.Add(cd0);
            }

            //MainView.ShowGridLines = true;
        }

        #endregion

        public void RefreshData()
        {
            this.refreshTimer.Interval = new TimeSpan(0, 0, 2);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();
            List<CategoryInfo> categoryList = new List<CategoryInfo>();
            categoryList.Add(new CategoryInfo { Name = "夹具监控", Value = 0 });
            categoryList.Add(new CategoryInfo { Name = "取放监控", Value = 1 });
            this.comTepy.ItemsSource = categoryList;
            comTepy.DisplayMemberPath = "Name";
            comTepy.SelectedValuePath = "Value";
            this.comTepy.SelectedIndex = 0;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (MainView.RowDefinitions.Count < 1)
            {
                this.CreateView();                
            }
            this.RefreshData();
        }

        private void Page_UnLoaded(object sender, RoutedEventArgs e)
        {
            this.refreshTimer.Stop();           
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
        
                var fixtureFurnaceList = fixtureFurnaceMainDB.GetAllData().OrderBy(x => x.FFMNumber).ToList();

            for (int i = 0; i < fixtureFurnaceList.Count(); i++)
            {
                if (fixtureFurnaceList[i].FFMCreateType == 1)//给炉子赋值
                {
                    TengDa.UserControls.OvenView ov = FindChild<OvenView>(this.canvas, "button" + fixtureFurnaceList[i].FFMId);
                    OvenViewModel model = null;
                    if (ov != null)
                    {
                        model = ov.DataContext as OvenViewModel;

                        if ((int)fixtureFurnaceList[i].FFMState == 2)
                        {
                            model.TipInfo = string.Format("报警信息：{0}", "测试报警数据1");
                        }
                        model.ImgSource = GetAlarmPath((int)fixtureFurnaceList[i].FFMState);

                        //
                    }
                    if (model != null)
                    {
                        RefreshFixtureFurnaceDetail(model, fixtureFurnaceList[i].FFMId);
                    }
                }
                else if (fixtureFurnaceList[i].FFMCreateType == 2)//上下料平台赋值
                {
                    TengDa.UserControls.MachinePlatform mp = FindChild<MachinePlatform>(this.canvas, "button" + fixtureFurnaceList[i].FFMId);
                    MachinePlatformModel model = null;
                    if (mp != null)
                    {
                        model = mp.DataContext as MachinePlatformModel;

                        if ((int)fixtureFurnaceList[i].FFMState == 2)
                        {
                            model.TipInfo = string.Format("报警信息：{0}", "测试报警数据");
                        }
                        model.ImgSource = GetAlarmPath((int)fixtureFurnaceList[i].FFMState);
                        //
                    }
                    if (model != null)
                    {
                        RefreshMachine(model, fixtureFurnaceList[i].FFMId);
                    }
                }

            }

            //机器人运行状态
            var RobotStateList = robotStateInfoDB.GetAllData().FirstOrDefault();
            if (RobotStateList != null)
            {
                ChangeImgAddress(1,Convert.ToInt32(RobotStateList.RSIPosition)*2, Convert.ToInt32(RobotStateList.RSIMoveState));
            }


        }

        /// <summary>
        /// 返回报警状态灯信息
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        string GetAlarmPath(int state)
        {
            string alarmPath = string.Empty;
            switch (state)
            {
                case 0:
                    alarmPath= "pack://application:,,,/TengDa.UserControls;component/image/greenlight.png";
                    break;
                case 1:
                    alarmPath = "pack://application:,,,/TengDa.UserControls;component/image/yellowlight.png";
                    break;
                case 2:
                    alarmPath = "pack://application:,,,/TengDa.UserControls;component/image/redlight.png";
                    break;
                default:
                     alarmPath = "pack://application:,,,/TengDa.UserControls;component/image/redlight.png";
                    break;
            }
            return alarmPath;
        }

        /// <summary>
        /// 刷炉子明细信息
        /// </summary>
        /// <param name="FFMID"></param>
        void RefreshFixtureFurnaceDetail(OvenViewModel model,int FFMID=0)
        {
            var fixtureFurnaceDetailList = fixtureFurnaceDetailDB.GetAllDataByFFDID(FFMID);
            for (int i = 0; i < fixtureFurnaceDetailList.Count(); i++)
            {


                if (!string.IsNullOrEmpty(fixtureFurnaceDetailList[i].CABarCode))
                {
                    model.TempBtnData[i / 2].IsShow = true;
                    model.TempBtnData[i / 2].ProgressText = string.Format("{0}% ", 70);
                    model.TempBtnData[i / 2].ProgressValue = 90;
                    model.TempBtnData[i / 2].ProgressTitel = string.Format("完成{0}% 剩余{1}min", 70, 100);
                }
                if (this.comTepy.SelectedIndex == 0)
                {
                    if (fixtureFurnaceDetailList[i].FFDNumber % 2 == 0)//右边按钮赋值
                    {
                        int SS = Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1;
                        model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].BackBtnInfo.TempNum = fixtureFurnaceDetailList[i].CABarCode;
                        model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].BackBtnInfo.ToolTipText = fixtureFurnaceDetailList[i].CABarCode;
                        //model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDLayer) -1].AheadBtnInfo = SetButtonState(Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDState), fixtureFurnaceDetailListByNumber[i].CABarCode);

                    }
                    else//左边按钮赋值
                    {
                        int SS = Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 3;
                        model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].AheadBtnInfo.TempNum = fixtureFurnaceDetailList[i].CABarCode;
                        model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].AheadBtnInfo.ToolTipText = fixtureFurnaceDetailList[i].CABarCode;
                        //model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDLayer) - 1].BackBtnInfo = SetButtonState(Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDState), fixtureFurnaceDetailListByNumber[i].CABarCode);

                    }
                }
                else if (this.comTepy.SelectedIndex == 1)
                {
                    string sss = fixtureFurnaceDetailList[i].FFMName.Substring(0, 1);
                    if (TengDa.Communication.DataSource.ChuckingStatus[Convert.ToInt16(fixtureFurnaceDetailList[i].FFMName.Substring(0, 1)) - 1] != null)
                    {
                        if (fixtureFurnaceDetailList[i].FFDNumber % 2 == 0)//右边按钮赋值
                        {
                            string SS = TengDa.Communication.DataSource.ChuckingStatus[Convert.ToInt16(fixtureFurnaceDetailList[i].FFMName.Substring(0, 1)) - 1][i];
                            model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].BackBtnInfo.TempNum = TengDa.Communication.DataSource.ChuckingStatus[Convert.ToInt16(fixtureFurnaceDetailList[i].FFMName.Substring(0, 1)) - 1][i];
                            model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].BackBtnInfo.ToolTipText = fixtureFurnaceDetailList[i].CABarCode;
                        }
                        else
                        {
                            string SS = TengDa.Communication.DataSource.ChuckingStatus[Convert.ToInt16(fixtureFurnaceDetailList[i].FFMName.Substring(0, 1)) - 1][i];

                            model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].AheadBtnInfo.TempNum = TengDa.Communication.DataSource.ChuckingStatus[Convert.ToInt16(fixtureFurnaceDetailList[i].FFMName.Substring(0, 1)) - 1][i];
                            model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].AheadBtnInfo.ToolTipText = fixtureFurnaceDetailList[i].CABarCode;
                        }

                    }
                }
            }
        }

        void RefreshMachine(MachinePlatformModel model, int FFMID = 0)
        {
            try
            {
                var fixtureFurnaceDetailList = fixtureFurnaceDetailDB.GetAllDataByFFDID(FFMID);
                for (int i = 0; i < fixtureFurnaceDetailList.Count(); i++)
                {
                    if (fixtureFurnaceDetailList[i].FFDNumber % 2 == 0)//右边按钮赋值
                    {
                        model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].MainBtnInfo.TempNum = fixtureFurnaceDetailList[i].CABarCode;
                        //model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDLayer) -1].AheadBtnInfo = SetButtonState(Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDState), fixtureFurnaceDetailListByNumber[i].CABarCode);
                    }
                    else//左边按钮赋值
                    {
                        model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].MainBtnInfo.TempNum = fixtureFurnaceDetailList[i].CABarCode;
                        model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailList[i].FFDLayer) - 1].MainBtnInfo.ToolTipText = fixtureFurnaceDetailList[i].CABarCode;
                        //model.TempBtnData[Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDLayer) - 1].BackBtnInfo = SetButtonState(Convert.ToInt32(fixtureFurnaceDetailListByNumber[i].FFDState), fixtureFurnaceDetailListByNumber[i].CABarCode);
                    }
                }
            } catch
            { }
        }

        public TempBtnInfoEx SetButtonState(int state,string nameCode)
        {
            TempBtnInfoEx tb = new TempBtnInfoEx();
            if (state == 10)//无夹具开门
            {
                tb.OpacityNum = 0.2;
                tb.BtnColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF377FED")); 
            }
            else if (state == 20)//无夹具关门
            {
                tb.OpacityNum = 1;
                tb.BtnColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF377FED")); ;
            }
            else if (state == 30)//无夹具关门
            {
                tb.OpacityNum = 0.2;
                tb.BtnColor = Brushes.Green;
            }
            else if (state == 40)//无夹具关门
            {
                tb.OpacityNum = 1;
                tb.BtnColor = Brushes.Green;
            }
            tb.TempNum = nameCode;
                return tb;
        }




        public T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // 如果子控件不是需查找的控件类型
                T childType = child as T;
                if (childType == null)
                {
                    // 在下一级控件中递归查找
                    foundChild = FindChild<T>(child, childName);

                    // 找到控件就可以中断递归操作 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // 如果控件名称符合参数条件
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // 查找到了控件
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }                 
      

  

        private void Uda_EBtnMethod(string code)
        {
            CustomMessageBox.Show(code, CustomMessageBoxButton.OK, CustomMessageBoxIcon.Question);
        }

        private void OvenView_EBtnMethod(TempBtnInfoEx btnInfo)
        {
            btnInfo.OpacityNum = 1;
            btnInfo.BtnColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF377FED")); //Brushes.Green; 
            CustomMessageBox.Show(btnInfo.TempNum, CustomMessageBoxButton.OK, CustomMessageBoxIcon.Question);
        }

        private void MachinePlatform_EBtnMethod(BtnInfoEx btnInfo)
        {
            string d = btnInfo.TempNum;
            //btnInfo.OpacityNum = 1;
            //btnInfo.BtnColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF377FED")); //Brushes.Green; 
            //MessageBox.Show(btnInfo.TempNum);
            CustomMessageBox.Show(btnInfo.TempNum, CustomMessageBoxButton.OK, CustomMessageBoxIcon.Question);

            //Login login = new Login();
            //login.ShowDialog();
        }
        /// <summary>
        /// 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OvenView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MachinePlatform mach = sender as MachinePlatform;
            if (mach != null)
            {
                int row = (int)mach.GetValue(Grid.RowProperty);
                int col = (int)mach.GetValue(Grid.ColumnProperty);
                ImgMachine.Visibility = Visibility.Visible;
                ChangeImgAddress(row > 0 ? row - 1 : row + 1, col, 1);


                //OvenViewModel model = mach.DataContext as OvenViewModel;
                //model.ImgSource = "pack://application:,,,/TengDa.UserControls;component/image/redlight.png";
                //model.TipInfo = "提示";
            }
            OvenView oven = sender as OvenView;
            if (oven != null)
            {
                int row= (int)oven.GetValue(Grid.RowProperty);
                int col =(int)oven.GetValue(Grid.ColumnProperty);
                ImgMachine.Visibility = Visibility.Visible;
                ChangeImgAddress(row > 0 ? row - 1 : row + 1, col, 2);

                OvenViewModel model = oven.DataContext as OvenViewModel;
                model.ImgSource = "pack://application:,,,/TengDa.UserControls;component/image/redlight.png";
                model.TipInfo = "提示";
            }
        }

        void ChangeImgAddress(int rowNum,int colNum,int index)
        {
            ImgMachine.Visibility = Visibility.Visible;
            ImgMachine.SetValue(Grid.RowProperty, rowNum); //设置按钮所在Grid控件的行
            ImgMachine.SetValue(Grid.ColumnProperty, colNum); //设置按钮所在Grid控件的列                
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            switch (index)
            {
                case 1:
                    bi.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/ToBottom.png", UriKind.Absolute);
                    break;
                case 2:
                    bi.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/ToTop.png", UriKind.Absolute);
                    break;
                case 3:
                    bi.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/ToCentre.png", UriKind.Absolute);
                    break;
                default:
                    bi.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/ToCentre.png", UriKind.Absolute);
                    break;
            }
            bi.EndInit();
            ImgMachine.Source = bi;

            BitmapImage biAlarm = new BitmapImage();          
            ImgMachineAlarm.Visibility = Visibility.Visible;
            ImgMachineAlarm.SetValue(Grid.RowProperty, rowNum); //设置按钮所在Grid控件的行
            ImgMachineAlarm.SetValue(Grid.ColumnProperty, colNum); //设置按钮所在Grid控件的列    
            biAlarm.BeginInit();
            switch (index)
            {
                case 0:
                    biAlarm.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/minGreenlight.png", UriKind.Absolute);
                    break;
                case 1:
                    biAlarm.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/minYellowlight.png", UriKind.Absolute);
                    break;
                case 2:
                    biAlarm.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/minRedlight.png", UriKind.Absolute);
                    break;
                default:
                    biAlarm.UriSource = new Uri("pack://application:,,,/TengDa.UserControls;component/image/minRedlight.png", UriKind.Absolute);
                    break;
            }
            biAlarm.EndInit();
            ImgMachineAlarm.Source = biAlarm;
        }

        public class CategoryInfo
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}
