using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Models.Entities
{
    public class FixtureFurnaceDetailEntity
    {
        #region Model
        private int _ffdid;
        private int? _ffdnumber;
        private int? _ffdlayer;
        private int? _caid;
        private int? _ffmid;
        private int? _ffdstate;
        private DateTime? _instovetime;
        private DateTime? _outstovetime;
        private int? _ffdisaccomplish;
        private int? _takecode;
        private int? _releasecode;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int FFDId
        {
            set { _ffdid = value; }
            get { return _ffdid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFDNumber
        {
            set { _ffdnumber = value; }
            get { return _ffdnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFDLayer
        {
            set { _ffdlayer = value; }
            get { return _ffdlayer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CAId
        {
            set { _caid = value; }
            get { return _caid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFMId
        {
            set { _ffmid = value; }
            get { return _ffmid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFDState
        {
            set { _ffdstate = value; }
            get { return _ffdstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InStoveTime
        {
            set { _instovetime = value; }
            get { return _instovetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutStoveTime
        {
            set { _outstovetime = value; }
            get { return _outstovetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FFDIsAccomplish
        {
            set { _ffdisaccomplish = value; }
            get { return _ffdisaccomplish; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? takeCode
        {
            set { _takecode = value; }
            get { return _takecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? releaseCode
        {
            set { _releasecode = value; }
            get { return _releasecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }

    [Serializable]
    public class FixtureFurnaceDetailEntityORMMapper : ClassMapper<FixtureFurnaceDetailEntity>
    {
        public FixtureFurnaceDetailEntityORMMapper()
        {
            base.Table("FixtureFurnaceDetail");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
