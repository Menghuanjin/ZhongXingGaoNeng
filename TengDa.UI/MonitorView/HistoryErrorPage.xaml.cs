using Microsoft.Win32;
using PagedList;
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
using TengDa.DB;
using TengDa.Model;
using TengDa.Models;
using TengDa.UserControls;

namespace TengDa.UI.MonitorView
{
    /// <summary>
    /// HistoryErrorPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryErrorPage : BasePage, IComponentConnector
    {
        private static List<BakingErrorRecord> berList { set; get; }
        private static BakingErrorRecordDB bermodel = new BakingErrorRecordDB();

        private static List<string> listStorey = new List<string>()
        {
                "上层","中层","下层"
        };
        private static List<string> listL = new List<string>()
        {
             "左侧","右侧"
        };
        public HistoryErrorPage()
        {
            InitializeComponent();
            this.pager.PageNumberChanged += new EventHandler<PageNumberChangedEventArgs>(this.Pager_PageNumberChanged);
            this.DataContext = this;
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
        private void Pager_PageNumberChanged(object sender, PageNumberChangedEventArgs e)
        {
           this.RefreshData(berList);
        }
        public void RefreshData(List<BakingErrorRecord> db)
        {
            if (db != null)
            {
                int index = db.Count % 10 > 0 ? 1 : 0;
                int test = db.Count / 10 + index;
                this.pager.Setup((IPagedData)new PagedData<object>()
                {
                   PageNumber = this.pager.PageNumber,
                    PageCount = test,
                    PageSize = 10,
                });
                IPagedList<BakingErrorRecord> pagerList = new PagedList.PagedList<BakingErrorRecord>(from m in db select m, this.pager.PageNumber, 10);
                PagedData<BakingErrorRecord> model = new PagedData<BakingErrorRecord>(pagerList);
                this.dataGrid.ItemsSource = model;
            }
        }
        private void buttQuery_Click(object sender, RoutedEventArgs e)
        {
            berList = bermodel.QueryErrorInfo(this.comMachine.SelectedIndex, TengDa.Communication.APPBLL.Convert(this.comStorey.SelectedIndex),  this.BeginTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), this.EndTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            this.RefreshData(berList);
        }

        private void buttOut_Click(object sender, RoutedEventArgs e)
        {
            if (berList == null)
                return;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "保存EXECL文件";
            saveFileDialog1.Filter = "Excel files(*.CSV)|*.CSV";//更改导出的类型
            SaveFileDialog saveFileDialog2 = saveFileDialog1;
            if (saveFileDialog2.ShowDialog() != Convert.ToBoolean(MessageBoxResult.OK))//打开一个选择路径保存窗口
                return;

            if (TengDa.Common.CSVUtil.Export(berList, saveFileDialog2.FileName))
                TengDa.UI.MessageBox.CustomMessageBox.Show("导出CSV文件成功", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
            else
                TengDa.UI.MessageBox.CustomMessageBox.Show("导出CSV文件失败", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Warning);

        }
    }
}
