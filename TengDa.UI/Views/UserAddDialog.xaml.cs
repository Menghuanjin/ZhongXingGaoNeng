
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
using System.Windows.Shapes;
using TengDa.Core;
using TengDa.DB;
using TengDa.Model.Entities;

namespace TengDa.UI
{
    /// <summary>
    /// UserAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UserAddDialog : Window, IComponentConnector
    {
        public UserAddDialog()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public class UserRoleItem
        {
            public int UserRoleID { get; set; }
            public bool Selected { get; set; }
            public string UserRoleName { get; set; }
        }

        public UsersEntity Model { get; set; }
        public List<UserRoleItem> userRoleStates;//用户权限列表
        //public int UserId { get; set; }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.Model == null)
                return;
            if (string.IsNullOrEmpty(this.Model.UserNumber))
            {
                MessageBox.CustomMessageBox.Show("请输入用户名!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                // MessageBox.Show("请输入用户名", "提示");

            }
            else if (string.IsNullOrEmpty(this.Model.UserName))
            {
                MessageBox.CustomMessageBox.Show("请输入姓名!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                // MessageBox.Show("请输入姓名", "提示");
            }
            else if (string.IsNullOrEmpty(this.Model.UserPwd))
            {
                MessageBox.CustomMessageBox.Show("请输入密码!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                // MessageBox.Show("请输入密码", "提示");
            }
            else if (this.Model.RoleId == 0)
            {
                MessageBox.CustomMessageBox.Show("请选择角色!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                // MessageBox.Show("请选择角色", "提示");
            }
            else
            {
                if (this.dialogTitle.Content.ToString().Contains("添加"))
                {
                    if ((new UsersDB()).Insert(Model)<1)
                    {
                       // MessageBox.Show("添加数据过程出现错误！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (!(new UsersDB()).Update(Model))
                    {
                       // MessageBox.Show("修改数据过程出现错误！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                this.DialogResult = new bool?(true);
                this.Close();
                
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!new Rect(new Point(20.0, 12.0), new Size(390.0, 28.0)).Contains(e.GetPosition(this))
                || e.ChangedButton != MouseButton.Left)
                return;
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //RoleListResponse roleListResponse = BLL.User.UserManager.GetRoleList(new RoleListRequest() { PageNumber = 1, PageSize = int.MaxValue });
            this.ddlGender.ItemsSource = new List<string> { "男", "女" };// Gender.Female.ToArrayList();
            this.ddlRole.ItemsSource = AppContext.Current.RoleData;
            if (this.Model  != null)
            {
                this.dialogTitle.Content = "修改用户";
            }
            else
            {
                this.dialogTitle.Content = "添加用户";
                this.Model = new UsersEntity();
            }


            //UserRoleListResponse userRoleListResponse = BLL.User.SysManager.GetCraftsList(new UserRoleListRequest());
            //int[] numArray;
            //if (!string.IsNullOrEmpty(this.Model.CraftDIDs))
            //    numArray = ((IEnumerable<string>)this.Model.CraftDIDs.Split(',')).Select<string, int>(m => int.Parse(m)).ToArray<int>();
            //else
            //    numArray = new int[0];
            //int[] craftDIDs = numArray;
            //this.userRoleStates = userRoleListResponse.UserRole.Select<UserRoleModel, UserAddDialog.UserRoleItem>(m =>
            //{
            //    UserRoleItem craftStateItem = new UserAddDialog.UserRoleItem();
            //    craftStateItem.UserRoleID = m.UserRoleId;
            //    craftStateItem.UserRoleName = m.UserRoleName;
            //    int num = ((IEnumerable<int>)craftDIDs).Contains<int>(m.UserRoleId) ? 1 : 0;
            //    craftStateItem.Selected = num != 0;
            //    return craftStateItem;
            //}).ToList();
            //this.lstCrafts.ItemsSource = this.userRoleStates;
            this.DataContext = this.Model;
        }
    }
    
}

