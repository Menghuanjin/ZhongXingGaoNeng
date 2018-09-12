using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Model.UM
{
  public  class UpwardMaterialDetail
    {
        /// <summary>
        /// UpwardMaterialDetail:实体类(属性说明自动提取数据库字段的描述信息)
        /// </summary>
            #region Model
            private int _umdid;
            private int? _ummid;
            private string _electriccore;
            private DateTime? _sweepcodetime;
            private string _remark;
            /// <summary>
            /// 
            /// </summary>
            public int UMDID
            {
                set { _umdid = value; }
                get { return _umdid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? UMMID
            {
                set { _ummid = value; }
                get { return _ummid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string ElectricCore
            {
                set { _electriccore = value; }
                get { return _electriccore; }
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
            public string Remark
            {
                set { _remark = value; }
                get { return _remark; }
            }
            #endregion Model
        }
    }
