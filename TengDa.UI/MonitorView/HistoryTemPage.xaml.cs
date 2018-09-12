using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
using TengDa.Model;
using TengDa.UserControls;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace TengDa.UI.MonitorView
{
    /// <summary>
    /// HistoryTemPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryTemPage : BasePage
    {
        private static TengDa.DB.TemDB model = new DB.TemDB();
        private static List<BakingTem> list { set; get; }
        private static List<string> listStorey = new List<string>()
        {
                "上层","中层","下层"
        };
        private static List<string> listL = new List<string>()
        {
             "左侧","右侧"
        };
        public HistoryTemPage()
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
            for (int i = 0; i < listL.Count; i++)
            {
                this.comDirection.Items.Add(listL[i]);
            }
            this.comMachine.SelectedIndex = 0;
            this.comStorey.SelectedIndex = 0;
            this.comDirection.SelectedIndex = 0;
        }

        private void buttQuery_Click(object sender, RoutedEventArgs e)
        {
            list = model.QueryTemData(this.comMachine.SelectedIndex, TengDa.Communication.APPBLL.Convert(this.comStorey.SelectedIndex), this.comDirection.SelectedIndex, this.BeginTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), this.EndTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            this.dgtOnlineRecord.ItemsSource = list;
        }

        private void buttOut_Click(object sender, RoutedEventArgs e)
        {
            if (list == null)
                return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Title = "保存EXECL文件";
            saveFileDialog1.Filter = "Excel files(*.CSV)|*.CSV";//更改导出的类型
            SaveFileDialog saveFileDialog2 = saveFileDialog1;
            if (saveFileDialog2.ShowDialog() != Convert.ToBoolean(MessageBoxResult.OK))//打开一个选择路径保存窗口
                return;
        
            if (TengDa.Common.CSVUtil.Export(list, saveFileDialog2.FileName))                   
                TengDa.UI.MessageBox.CustomMessageBox.Show("导出CSV文件成功", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
            else   
                TengDa.UI.MessageBox.CustomMessageBox.Show("导出CSV文件失败", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Warning);
            
        }
    }
}
