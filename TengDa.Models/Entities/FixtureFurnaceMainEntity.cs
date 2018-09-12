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
    public class FixtureFurnaceMainEntity : BaseModel
    {
        #region Model
        private int _ffmid;
        private int? _ffmstate;
        private string _ffmnumber;
        private string _ffmname;
        private int? _ffmcountlayer;
        private int? _ffmisavailable;
        private int? _ffmberthposition;
        private DateTime? _updatetime;
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private int? _ffmrowsort;
        private int? _ffmcreatetype;
        /// <summary>
        /// 
        /// </summary>
        public int FFMId
        {
            set { _ffmid = value; }
            get { return _ffmid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFMState
        {
            set { _ffmstate = value; }
            get { return _ffmstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FFMNumber
        {
            set { _ffmnumber = value; }
            get { return _ffmnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FFMName
        {
            set { _ffmname = value; }
            get { return _ffmname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFMCountLayer
        {
            set { _ffmcountlayer = value; }
            get { return _ffmcountlayer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFMIsAvailable
        {
            set { _ffmisavailable = value; }
            get { return _ffmisavailable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFMBerthPosition
        {
            set { _ffmberthposition = value; }
            get { return _ffmberthposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
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
        public int? FFMRowSort
        {
            set { _ffmrowsort = value; }
            get { return _ffmrowsort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFMCreateType
        {
            set { _ffmcreatetype = value; }
            get { return _ffmcreatetype; }
        }
        #endregion Model
    }
    [Serializable]
    public class FixtureFurnaceMainEntityORMMapper : ClassMapper<FixtureFurnaceMainEntity>
    {
        public FixtureFurnaceMainEntityORMMapper()
        {
            base.Table("FixtureFurnaceMain");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }

}
