using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TengDa.Common
{
    public class ObjectJsonHelp
    {
        /// <summary>
        /// 出站返回json(水含量解析)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static OutRootObject OutReturn(string str)
        {
            return JsonConvert.DeserializeObject<OutRootObject>(str);
        }    
        /// <summary>
        /// 出站Json字段
        /// </summary>
        public class OutRootObject
        {
            /// <summary>
            /// 设备  
            /// </summary>
            public string RESOURCE { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public string USER_ID { get; set; }
            /// <summary>
            /// 料号( 需要测水含量的电芯号)
            /// </summary>
            public string CONTAINER_ID { get; set; }
            /// <summary>
            /// 是否是第三次出站(结果值固定Y (第三次出站)N(不是第三次))
            /// </summary>
            public string IS_THREE_TIME { get; set; }
            /// <summary>
            /// 水含量返回解析
            /// </summary>
            public string INSPECT_RESULT { get; set; }

        }
        /// <summary>
        /// 进站Json字段
        /// </summary>
        public class InRootObject
        {
            /// <summary>
            /// 设备编号
            /// </summary>
            public string RESOURCE { get; set; }
            /// <summary>
            /// 操作
            /// </summary>
            public string ACTION { get; set; }
            /// <summary>
            /// 料框号
            /// </summary>
            public string CONTAINER_ID { get; set; }
            /// <summary>
            /// 条码框
            /// </summary>
            public List<sFC_LIST> SFC_LIST { get; set; }
        }
        public class nC_CODE_LIST//所有的条码在这里添加
        {
            public string NC_CODE { get; set; }
        }
        public class sFC_LIST
        {
            public string SFC { get; set; }
            public List<nC_CODE_LIST> NC_CODE_LIST { get; set; }
        }
        /// <summary>
        /// 机器Json字段
        /// </summary>
        public class RootObject
        {
            public List<rESOURCE_LIST> RESOURCE_LIST { set; get; }
        }
        public class rESOURCE_LIST
        {
            /// <summary>
            /// MES设备号
            /// </summary>
            public string RESOURCE { get; set; }
            /// <summary>
            /// 设备状态
            /// </summary>
            public string STATUS { get; set; }

        }
    }
}
