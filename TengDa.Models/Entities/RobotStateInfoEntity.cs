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
    public class RobotStateInfoEntity : BaseModel
    {
        #region Model
        private int _rsiid;
        private int? _rsinumber;
        private int? _rsimovestate;
        private int? _rsiposition;
        private DateTime? _updatetime;
        private int? _rsiworkingstate;
        /// <summary>
        /// 
        /// </summary>
        public int RSIId
        {
            set { _rsiid = value; }
            get { return _rsiid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RSINumber
        {
            set { _rsinumber = value; }
            get { return _rsinumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RSIMoveState
        {
            set { _rsimovestate = value; }
            get { return _rsimovestate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RSIPosition
        {
            set { _rsiposition = value; }
            get { return _rsiposition; }
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
        public int? RSIWorkingState
        {
            set { _rsiworkingstate = value; }
            get { return _rsiworkingstate; }
        }
        #endregion Model

    }

    [Serializable]
    public class RobotStateInfoEntityORMMapper : ClassMapper<RobotStateInfoEntity>
    {
        public RobotStateInfoEntityORMMapper()
        {
            base.Table("RobotStateInfo");
            //Map(f => f.UserID).Ignore();//设置忽略
            //Map(f => f.Name).Key(KeyType.Identity);//设置主键  (如果主键名称不包含字母“ID”，请设置)      
            AutoMap();
        }
    }
}
