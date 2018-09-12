using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TengDa.Common
{
  public    class SystemEnum
    {

        public static List<TengDa.Common.SystemEnum.MacChuStatus> mcsList = new List<MacChuStatus>();
        /// <summary>
        /// 夹具电池状态
        /// </summary>
        public enum MacChuStatus
        {
            [Description("未知")]
            Unknown =0,
            [Description("空夹具")]
            NullChu = 1,
            [Description("(满)未完成")]
            FullNO = 2,
            [Description("(满)已完成")]
            FullOK = 3,
            [Description("完成异常")]
            Abnormal = 4,
        }
    }
}
