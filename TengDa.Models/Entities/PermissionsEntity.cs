using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.Model;

namespace TengDa.Models.Entities
{
    [Serializable]
    public class PermissionsEntity:BaseModel
    {
        #region Model
        private int _permissionid;
        private string _permissionname;
        private string _permissioncode;
        private string _remark;
        private string _IconSource;
        private int? _displayorder;
        private int? _parentid;
        private int? _depth;
        /// <summary>
        /// 
        /// </summary>
        public int PermissionId
        {
            set { _permissionid = value; }
            get { return _permissionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PermissionName
        {
            set { _permissionname = value; }
            get { return _permissionname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IconSource
        {
            set { _IconSource = value; }
            get { return _IconSource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PermissionCode
        {
            set { _permissioncode = value; }
            get { return _permissioncode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Depth
        {
            set { _depth = value; }
            get { return _depth; }
        }
        #endregion Model
    }

    [Serializable]
    public class PermissionsEntityORMMapper : ClassMapper<PermissionsEntity>
    {
        public PermissionsEntityORMMapper()
        {
            base.Table("Permissions");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }

    public class MRoleTreeData : PermissionsEntity
    {
        protected bool? isChecked = false;
        /// <summary>
        /// 是否被勾选
        /// </summary>
        public bool? IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                this.RaisePropertyChanged("IsChecked");
                SetIsCheckedByParent(value);
                if (Parent != null)
                    Parent.SetIsCheckedByChild(value);
            }
        }

        protected bool isExpanded;
        /// <summary>
        /// 是否被展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                this.RaisePropertyChanged("IsExpanded");
            }
        }


        protected MRoleTreeData parent;
        /// <summary>
        /// 父项目
        /// </summary>
        public MRoleTreeData Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        protected List<MRoleTreeData> children = new List<MRoleTreeData>();
        /// <summary>
        /// 含有的子项目
        /// </summary>
        public List<MRoleTreeData> Children
        {
            get { return children; }
            set { children = value; }
        }


        /// <summary>
        /// 包含的对象
        /// </summary>
        public object Tag { get; set; }

        #region 业务逻辑, 如果你需要改成其他逻辑, 要修改的也就是这两行

        /// <summary>
        /// 子项目的isChecked改变了, 通知 是否要跟着改变 isChecked
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetIsCheckedByChild(bool? value)
        {
            if (this.isChecked == value)
            {
                return;
            }

            if (this.Children.All(c => c.IsChecked == true))
            {
                this.isChecked = true;
            }
            else if (this.Children.All(c => c.IsChecked == false))
            {
                this.isChecked = false;
            }
            else
            {
                this.isChecked = null;
            }

            this.RaisePropertyChanged("IsChecked");
            if (Parent != null) Parent.SetIsCheckedByChild(value);
        }

        /// <summary>
        /// 自己的isChecked改变了, 所有子项目都要跟着改变
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetIsCheckedByParent(bool? value)
        {
            this.isChecked = value;
            this.RaisePropertyChanged("IsChecked");
            foreach (var child in Children)
            {
                child.SetIsCheckedByParent(value);
            }
        }
        #endregion
    }
}
