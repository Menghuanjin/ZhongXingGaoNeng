using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Models.ViewModel
{
    public class FixtureFurnaceDetaiViewModel
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
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

        #region Model
        private int? _ffmstate;
        private string _ffmnumber;
        private string _ffmname;
        private int? _ffmcountlayer;
        private int _ffmisavailable;
        private int? _ffmberthposition;
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
        public int FFMIsAvailable
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
        #endregion

        #region Model
        private string _cabarcode;
        private int? _castate;
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
        public int? TakeCode
        {
            set { _takecode = value; }
            get { return _takecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReleaseCode
        {
            set { _releasecode = value; }
            get { return _releasecode; }
        }
        #endregion Model
    }
}
