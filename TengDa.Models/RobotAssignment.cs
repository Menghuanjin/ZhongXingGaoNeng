using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Model
{
    public  class RobotAssignment
    {
        #region Model
        private int _raid;
        private string _raname;
        private int? _ratype;
        private int? _ragrade;
        private int? _raalreadyexecute;
        private int? _rabegincode;
        private int? _raendcode;
        private string _rachuckingcode;
        private int? _rachuckinginfo;
        private DateTime? _rafound;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int RAID
        {
            set { _raid = value; }
            get { return _raid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAName
        {
            set { _raname = value; }
            get { return _raname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RAType
        {
            set { _ratype = value; }
            get { return _ratype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RAGrade
        {
            set { _ragrade = value; }
            get { return _ragrade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RAAlreadyExecute
        {
            set { _raalreadyexecute = value; }
            get { return _raalreadyexecute; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RABeginCode
        {
            set { _rabegincode = value; }
            get { return _rabegincode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RAEndCode
        {
            set { _raendcode = value; }
            get { return _raendcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RAChuckingCode
        {
            set { _rachuckingcode = value; }
            get { return _rachuckingcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RAChuckingInfo
        {
            set { _rachuckinginfo = value; }
            get { return _rachuckinginfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? RAFound
        {
            set { _rafound = value; }
            get { return _rafound; }
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
}
