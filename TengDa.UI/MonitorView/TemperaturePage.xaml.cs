using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
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
using TengDa.Model;
using TengDa.Models;
using TengDa.UserControls;


namespace TengDa.UI.MonitorView
{
    /// <summary>
    /// TemperaturePage.xaml 的交互逻辑
    /// </summary>
    public partial class TemperaturePage : BasePage
    {
        private DispatcherTimer refreshTimer = new DispatcherTimer();
        private TengDa.DB.ActualTimeDB modelDB = new DB.ActualTimeDB();
        private static List<string> listStorey = new List<string>()
            {
                "上层","中层","下层"
            };
        public TemperaturePage()
        {
            InitializeComponent();
            for (int i = 0; i < 9; i++)
            {
                this.comMachine.Items.Add(string.Format("{0}#真空炉", (1 + i).ToString()));
            }
            for (int i = 0; i < listStorey.Count; i++)
            {
                this.comStorey.Items.Add(listStorey[i]);
            }
            this.comMachine.SelectedIndex = 0;
            this.comStorey.SelectedIndex = 0;
        }
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.comStorey.SelectedIndex == 0)
            {
                DataExhibition(TengDa.Communication.DataSource.temleftListA[this.comMachine.SelectedIndex], TengDa.Communication.DataSource.temRightListA[this.comMachine.SelectedIndex]);
            }
            else if (this.comStorey.SelectedIndex == 1)
            {
                DataExhibition(TengDa.Communication.DataSource.temleftListB[this.comMachine.SelectedIndex], TengDa.Communication.DataSource.temRightListB[this.comMachine.SelectedIndex]);
            }
            else if (this.comStorey.SelectedIndex == 2)
            {
                DataExhibition(TengDa.Communication.DataSource.temleftListC[this.comMachine.SelectedIndex], TengDa.Communication.DataSource.temRightListC[this.comMachine.SelectedIndex]);
            }
            ActualTimeTem(this.comMachine.SelectedIndex, this.comStorey.SelectedIndex);
        }
        private void DataExhibition(List<string> listA, List<string> listB)
        {
            if (listA != null)
            {
                for (int j = 0; j < 20; j++)
                {
                    var temShow = GetControlObject<TextBox>("textLeft" + (1 + j).ToString());
                    temShow.Text = listA[j];
                }
            }
            if (listB != null)
            {
                for (int j = 0; j < 20; j++)
                {
                    var temShow = GetControlObject<TextBox>("textRight" + (1 + j).ToString());
                    temShow.Text = listB[j];
                }
            }
        }
        private void ActualTimeTem(int machine, int storey)

        {
            try
            {        
                List<ActualTimeTem> list = modelDB.GetBakingTem(machine, storey);
                if (list == null)
                    return;
                this.temperatureChart.SeriesColors = App.SeriesColors;
                CartesianChart cartesianChart = this.temperatureChart;
                SeriesCollection seriesCollection = new SeriesCollection();
                LineSeries columnSeries = new LineSeries();//LineSeries曲线
                columnSeries.Title = "当前温度：";
                columnSeries.Values = new ChartValues<double>(list.Select(x => Convert.ToDouble(x.K1))); ;
                seriesCollection.Add(columnSeries);
                int[] array = list.Select(m => m.BTime).ToArray();
                //AxisX: CraftLabels 工艺         AxisY: CraftFormatter 次数
                cartesianChart.DataContext = new
                {
                    TemperatureSeries = seriesCollection,
                    TemperatureLabels = array,
                    TemperatureFormatter = (Func<ChartPoint, string>)(value => value.ToString())
                };
            }
            catch { }

        }
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            this.refreshTimer.Interval = new TimeSpan(0, 0, 5);
            this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            this.refreshTimer.Start();
        }
        public T GetControlObject<T>(string controlName)
        {
            try
            {
                Type type = this.GetType();
                FieldInfo fieldInfo = type.GetField(controlName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                if (fieldInfo != null)
                {
                    T obj = (T)fieldInfo.GetValue(this);
                    return obj;
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        private void BasePage_Unloaded_1(object sender, RoutedEventArgs e)
        {
            this.refreshTimer.Stop();
        }
    }
}
