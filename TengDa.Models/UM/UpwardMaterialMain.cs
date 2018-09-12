using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Model
{
  public  class UpwardMaterialMain : BaseModel
    {
        #region Model
        private int _umid;
        private string _fixturebarcode;
        private int? _line;
        private int? _fixtureposition;
        private int? _sweepcodenumber;
        private DateTime? _sweepcodetime;
        private DateTime? _createtime;
        private DateTime? _instovetime;
        private DateTime? _outstovetime;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int UMID
        {
            set { _umid = value; }
            get { return _umid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FixtureBarCode
        {
            set { _fixturebarcode = value; }
            get { return _fixturebarcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? line
        {
            set { _line = value; }
            get { return _line; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FixturePosition
        {
            set { _fixtureposition = value; }
            get { return _fixtureposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SweepCodeNumber
        {
            set { _sweepcodenumber = value; }
            get { return _sweepcodenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SweepCodeTime
        {
            set { _sweepcodetime = value; }
            get { return _sweepcodetime; }
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
        public DateTime? inStoveTime
        {
            set { _instovetime = value; }
            get { return _instovetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? outStoveTime
        {
            set { _outstovetime = value; }
            get { return _outstovetime; }
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
