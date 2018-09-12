using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.WebService.MachineService;
using TengDa.WebService.TestSystemService;


namespace TengDa.WebService
{
  public  class ServeHelp
    {
        public static ServeHelp Service = new ServeHelp();
        public static ServeHelp WebService
        {
            get { return Service; }
        }
        /// <summary>
        /// 測試系統接口
        /// </summary>
        /// <returns></returns>
        public   bool TestSystemService()
        {    
            getResourceDescriptionResponse ReturnInfo = new getResourceDescriptionResponse();
            MachineAccessTestServiceService Service = new MachineAccessTestServiceService();  
            Service.Url = "http://10.10.156.11:51000/sapdevwebservice/MachineAccessTestServiceService?wsdl";
            Service.Credentials = new System.Net.NetworkCredential(Core.AppContext.Current.MesModel.Account, Core.AppContext.Current.MesModel.Pwd, null);
            machineAccessTestRequest test = new machineAccessTestRequest()
            {
                resource = "3BKX0001",
                site = "1003",
            };
            getResourceDescription grt = new getResourceDescription()
            {
                pRequest = test,
            };
            ReturnInfo = Service.getResourceDescription(grt);
            return ReturnInfo.@return.status.ToUpper() == "TRUE" ? true : false;
        }
        /// <summary>
        /// 烘烤进站接口
        /// </summary>
        /// <param name="strJson">Json字符串</param>
        public  bool InService(string strJson)
        {
            ExecutingServiceService Service = new ExecutingServiceService()
            {
                Url = Core.AppContext.Current.MesModel.InUrl,
                Credentials = new System.Net.NetworkCredential(Core.AppContext.Current.MesModel.Account, Core.AppContext.Current.MesModel.Pwd, null),
            };
            executingServiceRequest Request = new executingServiceRequest()
            {
                site = Core.AppContext.Current.MesModel.InSite,//工厂代号
                serviceCode = Core.AppContext.Current.MesModel.InRESOURCE,//接口表示
                data = strJson,
            };
            execute ex = new execute() { pRequest = Request, };
            executeResponse ReturnData = new executeResponse();
            ReturnData = Service.execute(ex);
            return ReturnData.@return.status.ToUpper() == "TRUE"? true : false;
        }
        /// <summary>
        /// 烘烤出站接口()
        /// </summary>
        /// <param name="strJson">Json字符串</param>
        public executeResponse OutService(string strJson)
        {
            ExecutingServiceService Service = new ExecutingServiceService()
            {
                Url = Core.AppContext.Current.MesModel.OutUrl,
                Credentials = new System.Net.NetworkCredential(Core.AppContext.Current.MesModel.Account, Core.AppContext.Current.MesModel.Pwd, null),
            };       
            executingServiceRequest Request = new executingServiceRequest()
            {
                site = Core.AppContext.Current.MesModel.OutSite,//工厂代号
                serviceCode = Core.AppContext.Current.MesModel.OutServiceCode,//接口表示
                data = strJson,
            };
            execute ex = new execute() { pRequest = Request, };
            return  Service.execute(ex);
        }



        ///// <summary>
        ///// 烘烤出站接口
        ///// </summary>
        ///// <param name="Lot">托盘条码</param>
        ///// <returns></returns>
        //public bool OutServe(string Lot)
        //{
        //    BatteriesInAndOutStationServiceService Out= new BatteriesInAndOutStationServiceService();
        //   BatteriesInAndOutStatActionResponse ReturnOut = new BatteriesInAndOutStatActionResponse();
        //    Out.Url = "http://10.10.156.11:51000/sapdevwebservice/BatteriesInAndOutStationServiceService?wsdl";
        //    Out.Credentials = new System.Net.NetworkCredential("sapint", "sap12345", null);
        //    BatteriesInAndOutStatAction Action = new BatteriesInAndOutStatAction();
        //    batteriesInAndOutStationRequest Request = new batteriesInAndOutStationRequest();
        //    Request.site = "1003";
        //    Request.resource = "3BKX0001";
        //    Request.processLot = Lot;//"A10030200007";
        //    Action.pRequest = Request;
        //    ReturnOut = Out.BatteriesInAndOutStatAction(Action);
        //    return ReturnOut.@return.status.ToUpper() == "TRUE"  ? true : false;

        //}
        /// <summary>
        /// 机器运行状态接口
        /// </summary>
        /// <param name="strJson">strJson字符串</param>
        /// <returns></returns>
        public bool EquipmentService(string strJson)
        {
            ExecutingServiceService Service = new ExecutingServiceService()
            {
                Url = Core.AppContext.Current.MesModel.MachineUrl,
                Credentials = new System.Net.NetworkCredential(Core.AppContext.Current.MesModel.Account, Core.AppContext.Current.MesModel.Pwd, null),
            };      
            executingServiceRequest Request = new executingServiceRequest()
            {
                site = Core.AppContext.Current.MesModel.EqSite,//工厂代号
                serviceCode = Core.AppContext.Current.MesModel.EqServiceCode,//接口表示
                data = strJson,
            };
            execute ex = new execute() { pRequest = Request,};
            executeResponse ReturnData = new executeResponse();
            ReturnData = Service.execute(ex);
            return ReturnData.@return.status.ToUpper() == "TRUE" ? true : false;
        }
    }
}
