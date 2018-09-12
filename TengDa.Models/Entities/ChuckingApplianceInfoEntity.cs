using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Models.Entities
{
    public class ChuckingApplianceInfoEntity
    {
        #region Model
        private int _caid;
        private string _cabarcode;
        private int? _castate;
        private DateTime? _updatetime;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int CAId
        {
            set { _caid = value; }
            get { return _caid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CABarCode
        {
            set { _cabarcode = value; }
            get { return _cabarcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CAState
        {
            set { _castate = value; }
            get { return _castate; }
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
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }

    [Serializable]
    public class UsersEntityORMMapper : ClassMapper<ChuckingApplianceInfoEntity>
    {
        public UsersEntityORMMapper()
        {
            base.Table("ChuckingApplianceInfo");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
