using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.Model;

namespace TengDa.Communication
{
    public class APPBLL
    {
        /// <summary>
        /// 左边上料
        /// </summary>
        public static UpwardMaterialMain ummLeftEntity { set; get; }
        /// <summary>
        /// 右边上料
        /// </summary>
        public static UpwardMaterialMain ummRightEntity { set; get; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public static List< TengDa.Model.BakingErrorInfo> errorModel { set; get; }

        public static  int comp { get; set; } = 0;
        public static string Convert(int get)
        {     
            string ss = string.Empty;
            switch (get)
            {
                case 0:
                    ss = "A";
                    break;
                case 1:
                    ss = "B";
                    break;
                case 2:
                    ss = "C";
                    break;
            }
            return ss;
        }
    }
}
