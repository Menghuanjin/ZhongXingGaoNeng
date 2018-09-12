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

namespace TengDa.UI.MESManager
{
    /// <summary>
    /// MESInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class MESInfoPage : BasePage, IComponentConnector
    {
        public MESInfoPage()
        {
            RefreshData();
            InitializeComponent();  
        }
        TengDa.DB.MesConfigureDB mesDB = new DB.MesConfigureDB();
        public void RefreshData()
        {
            Core.AppContext.Current.MesModel = mesDB.GetUserRolesById();
            this.DataContext = Core.AppContext.Current.MesModel;
        }

        private void buttRevise_Click(object sender, RoutedEventArgs e)
        {
            if (!(new MesConfigureDB()).Update(Core.AppContext.Current.MesModel))
            {
                MessageBox.CustomMessageBox.Show("修改数据过程出现错误！", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                return;
            }
            RefreshData();
        }

        private void buttQuery_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
