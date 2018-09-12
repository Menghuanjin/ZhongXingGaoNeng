using LiveCharts.Wpf;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
namespace TengDa.UI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static readonly List<Color> Colors = new List<Color>()
        {
          Color.FromRgb( 243,  67,  54),
          Color.FromRgb( 254,  192,  7),
          Color.FromRgb( 96,  125,  138),
          Color.FromRgb( 0,  187,  211),
          Color.FromRgb( 232,  30,  99),
          Color.FromRgb( 254,  87,  34),
          Color.FromRgb( 63,  81,  180),
          Color.FromRgb( 204,  219,  57),
          Color.FromRgb( 0,  149,  135),
          Color.FromRgb( 76,  174,  80)
        };
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(this.Current_DispatcherUnhandledException);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.CurrentDomain_UnhandledException);
             TengDa.Communication.APPBLL.ummLeftEntity = TengDa.DB.UpwardMaterialMainDB.UMMD.GetAllNewData(1);
             TengDa.Communication.APPBLL.ummRightEntity = TengDa.DB.UpwardMaterialMainDB.UMMD.GetAllNewData(2);
            TengDa.Communication.APPBLL.errorModel = TengDa.DB.BakingErrorInfoDB.BEIF.GetAllData();
            App.SeriesColors.AddRange(App.Colors);
        }
        public static ColorsCollection SeriesColors = new ColorsCollection();

 

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    TengDa.Common.Log.Logs.Error("非UI线程全局异常：" + e.ToString());
                }
            }
            catch (Exception ex)
            {
                TengDa.Common.Log.Logs.Error("不可恢复的非UI线程全局异常：" + ex.ToString());
                MessageBoxX.Error("应用程序发生不可恢复的异常，将要退出！");
            }

            MessageBoxX.Error("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                TengDa.Common.Log.Logs.Error("UI线程全局异常" + e.Exception.ToString());
                e.Handled = true;
            }
            catch (Exception ex)
            {
                TengDa.Common.Log.Logs.Error("不可恢复的UI线程全局异常：" + ex.Message.ToString());
                MessageBoxX.Error("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.");
            }
        }

    } 

}

