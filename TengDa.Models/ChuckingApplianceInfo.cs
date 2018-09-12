using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Model
{
    public  class ChuckingApplianceInfo
    {
            #region Model
            private int _caid;
            private string _cabarcode;
            private int? _castate;
            private int? _caline;
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
            public int? CALine
            {
                set { _caline = value; }
                get { return _caline; }
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
}
