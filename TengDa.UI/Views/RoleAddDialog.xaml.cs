using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TengDa.Common;
using TengDa.DB;
using TengDa.Models.Entities;
using Util.Controls;
using TengDa.Model;


namespace TengDa.UI.Views
{
    /// <summary>
    /// RoleAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class RoleAddDialog : WindowBase, IComponentConnector
    {
        public RoleAddDialog()
        {
            InitializeComponent();
            this.WindowBase.Title = "添加角色";
             // this.dialogTitle.Content = "添加角色";
            RoleAddDialogModel.pageModel.RoleData = new RolesEntity(); ;
        }
        public RoleAddDialog(RolesEntity role)
        {
            InitializeComponent();
            this.WindowBase.Title = "修改角色";
            RoleAddDialogModel.pageModel.RoleData = JSONHelper.Deserialize<RolesEntity>(JSONHelper.Serialize(role));
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RoleAddDialogModel.pageModel.RoleData.RoleName))
            {
                int num = (int)MessageBox.CustomMessageBox.Show("请输入名称!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question); //(int)MessageBox.Show("请输入名称", "提示");
            }
            else
            {
                RoleAddDialogModel.pageModel.RoleData.PermissionCodes = string.Empty;
                ObservableCollection<MRoleTreeData> treeSource = RoleAddDialogModel.pageModel.TreeData;

                List<string> treeStr = new List<string>();
                foreach (var parent in treeSource)
                {
                    if (parent.IsChecked == null || parent.IsChecked.Value)
                    {
                        treeStr.Add(parent.PermissionCode);
                        if (parent.Children != null && parent.Children.Count > 0)
                        {
                            foreach (var child in parent.Children)
                            {
                                if (child.IsChecked == null || child.IsChecked.Value)
                                {
                                    treeStr.Add(child.PermissionCode);
                                }
                            }
                        }
                    }
                }
                RoleAddDialogModel.pageModel.RoleData.PermissionCodes = string.Join(",", treeStr);
                if (this.WindowBase.Title.ToString().Contains("修改"))
                {
                    if (!RoleAddDialogModel.pageModel.Update())
                    {
                        MessageBox.CustomMessageBox.Show("数据修改过程出现错误!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                        // MessageBox.Show("数据修改过程出现错误");
                        return;
                    }
                }
                else
                {
                    if (!RoleAddDialogModel.pageModel.Insert())
                    {
                        MessageBox.CustomMessageBox.Show("数据添加过程出现错误!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Question);
                        // MessageBox.Show("数据添加过程出现错误");
                        return;
                    }
                }
                this.DialogResult = new bool?(true);
                this.Close();
            }
        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            RoleAddDialogModel.pageModel.TreeData.Clear();
            this.DataContext = RoleAddDialogModel.pageModel;
            List<PermissionsEntity> perInfo = RoleAddDialogModel.pageModel.PermissionsDB.GetAll<PermissionsEntity>().ToList();
            List<PermissionsEntity> mainNodes = perInfo.Where((p) => p.ParentId == null).OrderBy((o) => o.DisplayOrder).ToList();
            foreach (var permissionNode in mainNodes)
            {
                MRoleTreeData mainInfo = JSONHelper.Deserialize<MRoleTreeData>(JSONHelper.Serialize(permissionNode));
                List<PermissionsEntity> childNodes = perInfo.Where((p) => p.ParentId == permissionNode.PermissionId).OrderBy((o) => o.DisplayOrder).ToList();
                RoleAddDialogModel.pageModel.TreeData.Add(mainInfo);
                if (childNodes.Count > 0)
                {
                    foreach (var child in childNodes)
                    {
                        MRoleTreeData childInfo = JSONHelper.Deserialize<MRoleTreeData>(JSONHelper.Serialize(child));
                        mainInfo.Children.Add(childInfo);
                        childInfo.Parent = mainInfo;
                        if (!string.IsNullOrEmpty(RoleAddDialogModel.pageModel.RoleData.PermissionCodes))
                        {
                            if (RoleAddDialogModel.pageModel.RoleData.PermissionCodes.Split(',').Any((p) => p.Equals(child.PermissionCode.ToString())))
                            {
                                childInfo.IsChecked = true;
                            }
                            else
                            {
                                childInfo.IsChecked = false;
                            }
                        }
                    }
                }
            }
        }
    }
        public class RoleAddDialogModel
        {
            public static RoleAddDialogModel pageModel = new RoleAddDialogModel();

            public RoleAddDialogModel()
            {
                RolesDB = new RolesDB();
                PermissionsDB = new PermissionsDB();
                TreeData = new ObservableCollection<MRoleTreeData>();
            }

            public ObservableCollection<MRoleTreeData> TreeData { get; set; }

            public RolesEntity RoleData { get; set; }

            public PermissionsDB PermissionsDB { get; set; }

            public RolesDB RolesDB { get; set; }

            public bool Update()
            {
                return RolesDB.Update(RoleData);
            }
            public bool Insert()
            {
                return RolesDB.Insert(RoleData) > 0;
            }
        }
}
