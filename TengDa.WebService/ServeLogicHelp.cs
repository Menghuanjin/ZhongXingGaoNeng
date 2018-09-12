using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.WebService.MachineService;

namespace TengDa.WebService
{
   public class ServiceLogicHelp
    {
        /// <summary>
        ///  将入站(一个料框的全部电芯条码，一次提交给MES)
        /// </summary>
        /// <param name="sfcInfo">电芯条码</param>
        /// <param name="container">料框号</param>
        /// <param name="ErrorInfo">该电芯是否在问题(如果本工序有其他问题，将这些问题添加到该电芯)</param>
        /// <returns></returns>
        public static bool InService(List<string> sfcList, string container, string ErrorInfo = "")
        {            
            List<TengDa.Common.ObjectJsonHelp.sFC_LIST> List = new List<TengDa.Common.ObjectJsonHelp.sFC_LIST>();
            for (int i = 0; i < sfcList.Count; i++)
            {
                Common.ObjectJsonHelp.nC_CODE_LIST NcCode = new Common.ObjectJsonHelp.nC_CODE_LIST();
                NcCode.NC_CODE = ErrorInfo;// "525421";//NC代码
                List<Common.ObjectJsonHelp.nC_CODE_LIST> NClist = new List<Common.ObjectJsonHelp.nC_CODE_LIST>();
                NClist.Add(NcCode);
                List.Add(new Common.ObjectJsonHelp.sFC_LIST { SFC = sfcList[i], NC_CODE_LIST = NClist });
            }
            TengDa.Common.ObjectJsonHelp.InRootObject Json = new TengDa.Common.ObjectJsonHelp.InRootObject()
            {
                CONTAINER_ID = container,// "A10030200007";//料框号
                SFC_LIST = List,
                RESOURCE = "3BKX0001",//设备编号
                ACTION = "S",//操作
            };
            return ServeHelp.Service.InService(Common.JSONHelper.SerializeTostr(Json));
        }
        /// <summary>
        /// 出站操作(将一个夹具所有的某一个电芯号提交给MES，返回 3 ，将间隔多长时间反复调接口)
        /// </summary>
        /// <param name="container">电芯号</param>
        /// <param name="three">是否第三次出站</param>
        public static string OutService(string container, string three = "N")//结果值固定Y (第三次出站)N(不是第三次)
        {
            executeResponse eR = new executeResponse();
            TengDa.Common.ObjectJsonHelp.OutRootObject Json = new TengDa.Common.ObjectJsonHelp.OutRootObject()
            {
                RESOURCE = "3BKX0001",
                USER_ID = "USER001",
                CONTAINER_ID = container,
                IS_THREE_TIME = three,
            };  
            eR = ServeHelp.Service.OutService(Common.JSONHelper.SerializeTostr(Json));//调用接口
            if (eR.@return.status.ToUpper() == "TRUE" && !string.IsNullOrEmpty(eR.@return.returnList))//调用成功
            {
                //解析返回的Json
                Json = TengDa.Common.ObjectJsonHelp.OutReturn(eR.@return.returnList);
            }//1 合格 2 不合格 3无数据 4 报废
            return Json.INSPECT_RESULT;

        }
        /// <summary>
        /// 机器设备状态(当某一台设备发生不一样的状态时候，提交一次给MES)
        /// </summary>
        /// <param name="equipment">设备号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static bool EquipmentService(string equipment, string status)
        {        
            List<TengDa.Common.ObjectJsonHelp.rESOURCE_LIST> List = new List<TengDa.Common.ObjectJsonHelp.rESOURCE_LIST>();
            TengDa.Common.ObjectJsonHelp.rESOURCE_LIST resource = new TengDa.Common.ObjectJsonHelp.rESOURCE_LIST()
            {
                RESOURCE = equipment,// "2BXCJ0001";
                STATUS = status,//"0";0:待机（停机），1:运行，2:报警，4：维修
            };
            List.Add(resource);
            TengDa.Common.ObjectJsonHelp.RootObject Json = new TengDa.Common.ObjectJsonHelp.RootObject(){ RESOURCE_LIST = List,};
            return ServeHelp.Service.EquipmentService(Common.JSONHelper.SerializeTostr(Json));//调用接口
        }
    }
}
