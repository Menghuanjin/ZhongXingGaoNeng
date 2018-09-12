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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TengDa.UserControls
{
    /// <summary>
    /// UpDownArticle.xaml 的交互逻辑
    /// </summary>
    public partial class UpDownArticle : Grid
    {
        public delegate void DgBtnMethod(string code);
        public event DgBtnMethod EBtnMethod;
        public UpDownArticle()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (EBtnMethod != null)
            {
                EBtnMethod(btn.Name);
            }
        }
    }

    public class UpDownArticleModel
    {
        public string Code { get; set; }

        public string BtnName1 { get; set; }
        public string BtnName2 { get; set; }
        public string BtnName3 { get; set; }
    }
}
