using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Model
{
   public  class BakingErrorRecord
    {
        #region Model
        private int _bakingrecordid;
        private string _bbakingname;
        private string _brecordcon;
        private DateTime? _brecordtime;
        /// <summary>
        /// 
        /// </summary>
        public int BakingRecordID
        {
            set { _bakingrecordid = value; }
            get { return _bakingrecordid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BBakingName
        {
            set { _bbakingname = value; }
            get { return _bbakingname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BRecordCon
        {
            set { _brecordcon = value; }
            get { return _brecordcon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BRecordTime
        {
            set { _brecordtime = value; }
            get { return _brecordtime; }
        }
        #endregion Model

    }
}
