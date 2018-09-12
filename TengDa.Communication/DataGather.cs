using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TengDa.Communication;
using TengDa.Model;
using TengDa.Model.UM;
using System.Linq;

namespace TengDa.Communication
{
    public class DataGather
    { 
        
        private static Task[] threadList = new Task[9];

        private static Task[] umList = new Task[2];
        private static TaskFactory taskFactory = new TaskFactory();
        public static void loadingBasicInfo()
        {
            Task.Factory.StartNew(() =>
           {
               Thread.Sleep(5000);
               for (int i = 0; i < IPAddress.bakIPlist.Length; i++)
               {
                   threadList[i] = taskFactory.StartNew(BakingMaterial, i);
               }
               for (int i = 0; i < 2; i++)
               {
                   umList[i] = taskFactory.StartNew(UpwardMaterialMaterial, i);//上料

               }
               Task.Factory.StartNew(BakingTemManage);//温度写入数据库
               //  Task.Factory.StartNew(TengDa.Communication.Robot.RobotAssignment.ExecuteTheTask);//搬运机器人任务
           });
        }
        private static void BakingTemManage()
        {
            while (true)
            {
                for (int i = 0; i < IPAddress.bakIPlist.Length; i++)
                {
                    TengDa.Communication.DataGather.HandleTem(i, TengDa.Communication.DataSource.temleftListA[i], TengDa.Communication.DataSource.timeListA, TengDa.Communication.DataSource.vacListA, "A", 1);

                    TengDa.Communication.DataGather.HandleTem(i, TengDa.Communication.DataSource.temleftListA[i], TengDa.Communication.DataSource.timeListA, TengDa.Communication.DataSource.vacListA, "A", 2);

                    TengDa.Communication.DataGather.HandleTem(i, TengDa.Communication.DataSource.temleftListB[i], TengDa.Communication.DataSource.timeListB, TengDa.Communication.DataSource.vacListB, "B", 1);

                    TengDa.Communication.DataGather.HandleTem(i, TengDa.Communication.DataSource.temRightListB[i], TengDa.Communication.DataSource.timeListB, TengDa.Communication.DataSource.vacListB, "B", 2);

                    TengDa.Communication.DataGather.HandleTem(i, TengDa.Communication.DataSource.temleftListC[i], TengDa.Communication.DataSource.timeListC, TengDa.Communication.DataSource.vacListC, "C", 1);

                    TengDa.Communication.DataGather.HandleTem(i, TengDa.Communication.DataSource.temRightListC[i], TengDa.Communication.DataSource.timeListC, TengDa.Communication.DataSource.vacListC, "C", 2);
                }
                Thread.Sleep(60000);
            }
        }
        private static void HandleTem(int i, List<string> list, string[] time, string[] bv, string ss, int numb)
        {
            if (list != null && !string.IsNullOrEmpty(bv[i]) && !string.IsNullOrEmpty(time[i]))
            {
                if (list[0] != "0.0")
                {
                    GrtWriteTem(i, list, time[i], bv[i], ss, numb);
                }
            }
        }
        private static void GrtWriteTem(int i, List<string> list, string time, string bv, string ss, int numb)
        {
            TengDa.Model.BakingTem modelBK = new Model.BakingTem()
            {
                BEstablish = Convert.ToDateTime(DateTime.Now),
                BTime = Convert.ToInt16(time)
            };
            TengDa.Common.OperateEntity.SetValueByPropertyName("BVacuum", modelBK, bv);

            TengDa.Common.OperateEntity.SetValueByPropertyName("BBakingName", modelBK, string.Format(@"{0}#Baking-{1}-{2}", i + 1, ss, numb));

            for (int j = 0; j < 19; j++)
            {
                TengDa.Common.OperateEntity.SetValueByPropertyName("K" + (1 + j).ToString(), modelBK, list[j]);
                TengDa.Common.OperateEntity.SetValueByPropertyName("C" + (1 + j).ToString(), modelBK, list[30 + j]);
            }
            (new TengDa.DB.TemDB()).Insert(modelBK);
        }
        /// <summary>
        /// 真空炉
        /// </summary>
        /// <param name="num"></param>
        private static void BakingMaterial(object num)
        {
            try
            {
                TengDa.Communication.SocketBLL.bakingContent[(int)num] = TengDa.Communication.OMRONPLCAddress.ConnectPLC(IPAddress.bakIPlist[(int)num], 9600);
            }
            catch (Exception ex)
            {
                TengDa.Common.Log.Logs.Error(string.Format(@"{0}#炉子建立连接异常，详细内容：{1}", (int)num + 1, ex.Message));
            }
            if ( TengDa.Communication.SocketBLL.bakingContent[(int)num].Connected)
                TengDa.Common.Log.Logs.Info(string.Format("{0}#真空炉建立连接成功", (int)num + 1));

            else
                TengDa.Common.Log.Logs.Info(string.Format("{0}#真空炉建立连接失败", (int)num + 1));

            while (true)
            {
                BakingStartCollectingData((int)num);//基本内容
                BakingErrorInfoShow((int)num);//异常报警信息
                Thread.Sleep(10000);
            }
        }
        /// <summary>
        /// 读取各台PLC基本数据
        /// </summary>
        /// <param name="get"></param>
        private static void BakingStartCollectingData(int get)
        {
            if ( TengDa.Communication.SocketBLL.bakingContent[get].Connected)
            {
                //下层温度
                TengDa.Communication.DataSource.temleftListA[get] = TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiTem( TengDa.Communication.SocketBLL.bakingContent[get], 3831, 60);

                TengDa.Communication.DataSource.temRightListA[get] = TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiTem( TengDa.Communication.SocketBLL.bakingContent[get], 3891, 60);
                //中层温度
                TengDa.Communication.DataSource.temleftListB[get] = TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiTem( TengDa.Communication.SocketBLL.bakingContent[get], 3951, 60);

                TengDa.Communication.DataSource.temRightListB[get] = TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiTem( TengDa.Communication.SocketBLL.bakingContent[get], 4011, 60);
                //上层温度
                TengDa.Communication.DataSource.temleftListC[get] = TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiTem( TengDa.Communication.SocketBLL.bakingContent[get], 4071, 60);

                TengDa.Communication.DataSource.temRightListC[get] = TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiTem( TengDa.Communication.SocketBLL.bakingContent[get], 4131, 60);
                //运行时间
                TengDa.Communication.DataSource.timeListA[get] = TengDa.Communication.OMRONPLCAddress.ReadDM( TengDa.Communication.SocketBLL.bakingContent[get], "3599");

                TengDa.Communication.DataSource.timeListB[get] = TengDa.Communication.OMRONPLCAddress.ReadDM( TengDa.Communication.SocketBLL.bakingContent[get], "3643");

                TengDa.Communication.DataSource.timeListC[get] = TengDa.Communication.OMRONPLCAddress.ReadDM( TengDa.Communication.SocketBLL.bakingContent[get], "3643");

                TengDa.Communication.DataSource.ChuckingStatus[get] = TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiToEnum(TengDa.Communication.SocketBLL.bakingContent[get], 5001, 6); //每一台机夹具状态
                //真空
                List<string> list1 = TengDa.Communication.OMRONPLCAddress.ReadMulti( TengDa.Communication.SocketBLL.bakingContent[get], 3601, 2);

                string HH1 = TengDa.Communication.Panasonnic.DecimalConversion.Ten2Hex(list1[1]) + TengDa.Communication.Panasonnic.DecimalConversion.Ten2Hex(list1[0]);

                {
                    TengDa.Communication.DataSource.vacListA[get] = TengDa.Communication.OMRON.OMRONVacuumConvert.floatintStringToFloat(HH1).ToString();
                }
                List<string> list2 = TengDa.Communication.OMRONPLCAddress.ReadMulti( TengDa.Communication.SocketBLL.bakingContent[get], 3645, 2);

                string HH2 = TengDa.Communication.Panasonnic.DecimalConversion.Ten2Hex(list2[1]) + TengDa.Communication.Panasonnic.DecimalConversion.Ten2Hex(list2[0]);

                if (HH2.Length == 8)
                {
                    TengDa.Communication.DataSource.vacListB[get] = TengDa.Communication.OMRON.OMRONVacuumConvert.floatintStringToFloat(HH2).ToString();
                }
                List<string> list3 = TengDa.Communication.OMRONPLCAddress.ReadMulti( TengDa.Communication.SocketBLL.bakingContent[get], 3689, 2);

                string HH3 = TengDa.Communication.Panasonnic.DecimalConversion.Ten2Hex(list3[1]) + TengDa.Communication.Panasonnic.DecimalConversion.Ten2Hex(list3[0]);

                if (HH3.Length == 8)
                {
                    TengDa.Communication.DataSource.vacListC[get] = TengDa.Communication.OMRON.OMRONVacuumConvert.floatintStringToFloat(HH3).ToString();
                }
              // Thread.Sleep(3000);
            }
            else
            {
                TengDa.Common.Log.Logs.Info(string.Format("检测到{0}#真空炉已经断线，系统开始重新进行连接.....", get + 1));

                Thread.Sleep(30000);
                try
                {    //重新握手连接
                    BakingMaterial(get);
                }
                catch (Exception ex)
                {
                    TengDa.Common.Log.Logs.Error(string.Format("{0}#真空炉异常详细内容:{1}", get + 1, ex.Message));
                }
            }
        }


        public static void BakingErrorInfoShow(int num)
        {
            if ( TengDa.Communication.SocketBLL.bakingContent[num].Connected)
            {
                int ss = Convert.ToInt16(TengDa.Communication.OMRONPLCAddress.ReadDM( TengDa.Communication.SocketBLL.bakingContent[num], "5000"));

                if (ss == 2)//当前机器为异常红灯
                {                   
                    //上层
                    TengDa.Communication.DataGather.AnalyzeGetEoor(num, TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiToErrorInfo( TengDa.Communication.SocketBLL.bakingContent[num], 4872, 80), 1);

                    TengDa.Communication.DataGather.AnalyzeGetEoor(num, TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiToErrorInfo( TengDa.Communication.SocketBLL.bakingContent[num], 4991, 1), 1);
                    //中层
                    TengDa.Communication.DataGather.AnalyzeGetEoor(num, TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiToErrorInfo( TengDa.Communication.SocketBLL.bakingContent[num], 4752, 80), 2);

                    TengDa.Communication.DataGather.AnalyzeGetEoor(num, TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiToErrorInfo( TengDa.Communication.SocketBLL.bakingContent[num], 4992, 1), 2);
                    //下层
                    TengDa.Communication.DataGather.AnalyzeGetEoor(num, TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiToErrorInfo( TengDa.Communication.SocketBLL.bakingContent[num], 4632, 80), 3);

                    TengDa.Communication.DataGather.AnalyzeGetEoor(num, TengDa.Communication.OMRONPAnalyzeDMHelp.ReadMultiToErrorInfo( TengDa.Communication.SocketBLL.bakingContent[num], 4993, 1), 3);
                }
            }
        }
        private static void UpwardMaterialMaterial(Object num)
        {
            try
            {
                TengDa.Communication.SocketBLL.matContent[(int)num] = TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicConn(IPAddress.UPIPList[(int)num], 9094, 1000);
            }
            catch (Exception ex)
            {
                TengDa.Common.Log.Logs.Error(string.Format(@"{0}#上料平台异常，详细内容：{1}", (int)num + 1, ex.Message));
            }

            if (TengDa.Communication.SocketBLL.matContent[(int)num].Connected)
                TengDa.Common.Log.Logs.Info(string.Format("{0}#上料平台建立连接成功", (int)num + 1));

            else
                TengDa.Common.Log.Logs.Info(string.Format("{0}#上料平台建立连接失败", (int)num + 1));

            while (true)
            {
                if ((int)num == 0)
                {
                    LeftUpwardMaterialDispatch((int)num);
                }
                else if ((int)num == 1)
                {
                    RightUpwardMaterialDispatch((int)num);
                }

                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// 左上料
        /// </summary>
        /// <param name="dp"></param>
        private static void LeftUpwardMaterialDispatch(int dp)
        {
            if (TengDa.Communication.SocketBLL.matContent[dp].Connected)
            {
                int sweepCK = TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicReadDT(TengDa.Communication.SocketBLL.matContent[dp], 1000);

                if (sweepCK == 1 || sweepCK == 2 || sweepCK == 3)//1-3#夹具位请求扫码
                {
                    //调用扫码枪
                    string swCode = "test";

                    if (swCode.Contains("ERROR"))
                    {
                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},{1}#夹具位扫码失败，失败次数：{2}次", dp + 1, sweepCK, 1));
                    }

                    TengDa.Communication.Interface inApi = new Interface
                    {
                        sweInfo = swCode,
                        matLien = dp + 1,
                        swNow = DateTime.Now
                    };
                    //调用接口逻辑返回
                    int apiCode = 0;
                    if (apiCode == 0)//接口通过
                    {
                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1000, 0);

                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1000, 99);

                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},{1}#夹具位扫码成功，条码内容{2}提交MES校验成功，MES返回结果{3}", dp + 1, sweepCK, swCode, apiCode));

                        UpwardMaterialMain uppeModel = new UpwardMaterialMain()
                        {
                            FixtureBarCode = swCode,//夹具内容
                            line = dp + 1,//上料线
                            FixturePosition = sweepCK,//上料缓存位
                            SweepCodeNumber = 72,//上料数量
                            SweepCodeTime = DateTime.Now,
                            CreateTime = DateTime.Now,
                        };
                        //    (new TengDa.DB.UpwardMaterialMainDB()).Insert(uppeModel);
                        TengDa.Communication.APPBLL.ummLeftEntity = TengDa.DB.UpwardMaterialMainDB.UMMD.GetAllNewData(dp + 1);
                    }
                    else//扫码失败
                    {
                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},{1}#夹具位扫码成功，条码内容{2}提交MES校验失败，MES返回结果{3}", dp + 1, sweepCK, swCode, apiCode));

                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1000, 0);

                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1003, 1);
                    }
                    //扫电芯
                    int sweepBattery = TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicReadDT(TengDa.Communication.SocketBLL.matContent[dp], 1002);
                    if (sweepBattery == 1)
                    {
                        string ssCode = string.Empty;
                        //调用电芯扫码枪
                        ssCode = "test";

                        if (ssCode.Contains("ERROR"))
                        {
                            TengDa.Common.Log.Logs.Info(string.Format("上料线{0},扫电池码失败，失败次数：{1}次", dp + 1, sweepCK, 1));
                            for (int i = 0; i < 2; i++)
                            {         //调用电芯扫码枪
                                ssCode = "test";
                                if (ssCode.Contains("ERROR"))
                                {
                                    TengDa.Common.Log.Logs.Info(string.Format("上料线{0},扫电池码失败，失败次数：{1}次", dp + 1, 2 + i));
                                    if ((2 + i) >= 3)//超过扫码3次直接丢到NG位
                                    {
                                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1002, 0);
                                        //NG料
                                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1003, 2);

                                        return;
                                    }
                                }
                            }
                        }
                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},扫电池码成功，条码内容：{1}", dp + 1, ssCode));
                        TengDa.Communication.Interface inApi1 = new Interface
                        {
                            sweInfo = ssCode,
                            matLien = dp + 1,
                            swNow = DateTime.Now
                        };
                        //调用接口逻辑返回
                        int apiCode1 = 0;
                        if (apiCode1 == 0)
                        {
                            TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1002, 0);
                            //电芯OK
                            TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1003, 1);

                            UpwardMaterialDetail umdModel = new UpwardMaterialDetail()
                            {
                                ElectricCore = ssCode,
                                UMMID = TengDa.Communication.APPBLL.ummLeftEntity.UMID,
                                SweepCodeTime = DateTime.Now

                            };
                            //   (new TengDa.DB.UpwardMaterialDetailDB()).Insert(umdModel);

                            TengDa.Common.Log.Logs.Info(string.Format("上料线{0},条码内容{1}提交MES校验成功，MES返回结果{2}", dp + 1, ssCode, apiCode1));
                        }
                        else
                        {
                            TengDa.Common.Log.Logs.Info(string.Format("上料线{0},条码内容{1}提交MES校验失败，MES返回结果{2}", dp + 1, ssCode, apiCode1));
                        }
                    }
                }
            }
            else
            {
                TengDa.Common.Log.Logs.Info(string.Format("检测到{0}#上料已经断线，系统开始重新进行连接.....", dp + 1));

                Thread.Sleep(30000);
                try
                {    //重新握手连接
                    TengDa.Communication.SocketBLL.matContent[dp].Close();
                    TengDa.Communication.SocketBLL.matContent[dp] = TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicConn(IPAddress.UPIPList[dp], 9094, 500);
                }
                catch (Exception ex)
                {
                    TengDa.Common.Log.Logs.Error(string.Format("{0}#真空炉异常详细内容:{1}", dp + 1, ex.Message));
                }
            }
        }
        /// <summary>
        /// 右上料
        /// </summary>
        /// <param name="dp"></param>
        private static void RightUpwardMaterialDispatch(int dp)
        {
            if (TengDa.Communication.SocketBLL.matContent[dp].Connected)
            {
                int sweepCK = TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicReadDT(TengDa.Communication.SocketBLL.matContent[dp], 1000);

                if (sweepCK == 1 || sweepCK == 2 || sweepCK == 3)//1-3#夹具位请求扫码
                {
                    //调用扫码枪
                    string swCode = "test";

                    if (swCode.Contains("ERROR"))
                    {
                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},{1}#夹具位扫码失败，失败次数：{2}次", dp + 1, sweepCK, 1));
                    }

                    TengDa.Communication.Interface inApi = new Interface
                    {
                        sweInfo = swCode,
                        matLien = dp + 1,
                        swNow = DateTime.Now
                    };
                    //调用接口逻辑返回
                    int apiCode = 0;
                    if (apiCode == 0)//接口通过
                    {
                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1000, 0);

                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1000, 99);

                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},{1}#夹具位扫码成功，条码内容{2}提交MES校验成功，MES返回结果{3}", dp + 1, sweepCK, swCode, apiCode));

                        UpwardMaterialMain uppeModel = new UpwardMaterialMain()
                        {
                            FixtureBarCode = swCode,//夹具内容
                            line = dp + 1,//上料线
                            FixturePosition = sweepCK,//上料缓存位
                            SweepCodeNumber = 72,//上料数量
                            SweepCodeTime = DateTime.Now,
                            CreateTime = DateTime.Now,
                        };
                        //    (new TengDa.DB.UpwardMaterialMainDB()).Insert(uppeModel);
                        TengDa.Communication.APPBLL.ummLeftEntity = TengDa.DB.UpwardMaterialMainDB.UMMD.GetAllNewData(dp + 1);
                    }
                    else//扫码失败
                    {
                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},{1}#夹具位扫码成功，条码内容{2}提交MES校验失败，MES返回结果{3}", dp + 1, sweepCK, swCode, apiCode));

                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1000, 0);

                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1003, 1);
                    }
                    //扫电芯
                    int sweepBattery = TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicReadDT(TengDa.Communication.SocketBLL.matContent[dp], 1002);
                    if (sweepBattery == 1)
                    {
                        string ssCode = string.Empty;
                        //调用电芯扫码枪
                        ssCode = "test";

                        if (ssCode.Contains("ERROR"))
                        {
                            TengDa.Common.Log.Logs.Info(string.Format("上料线{0},扫电池码失败，失败次数：{1}次", dp + 1, sweepCK, 1));
                            for (int i = 0; i < 2; i++)
                            {         //调用电芯扫码枪
                                ssCode = "test";
                                if (ssCode.Contains("ERROR"))
                                {
                                    TengDa.Common.Log.Logs.Info(string.Format("上料线{0},扫电池码失败，失败次数：{1}次", dp + 1, 2 + i));
                                    if ((2 + i) >= 3)//超过扫码3次直接丢到NG位
                                    {
                                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1002, 0);
                                        //NG料
                                        TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1003, 2);

                                        return;
                                    }
                                }
                            }
                        }
                        TengDa.Common.Log.Logs.Info(string.Format("上料线{0},扫电池码成功，条码内容：{1}", dp + 1, ssCode));
                        TengDa.Communication.Interface inApi1 = new Interface
                        {
                            sweInfo = ssCode,
                            matLien = dp + 1,
                            swNow = DateTime.Now
                        };
                        //调用接口逻辑返回
                        int apiCode1 = 0;
                        if (apiCode1 == 0)
                        {
                            TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1002, 0);
                            //电芯OK
                            TengDa.Communication.Panasonnic.PanasonnicPLCAddress.PanasonnicWriteDT(TengDa.Communication.SocketBLL.matContent[dp], 1003, 1);

                            UpwardMaterialDetail umdModel = new UpwardMaterialDetail()
                            {
                                ElectricCore = ssCode,
                                UMMID = TengDa.Communication.APPBLL.ummLeftEntity.UMID,
                                SweepCodeTime = DateTime.Now

                            };
                            //   (new TengDa.DB.UpwardMaterialDetailDB()).Insert(umdModel);

                            TengDa.Common.Log.Logs.Info(string.Format("上料线{0},条码内容{1}提交MES校验成功，MES返回结果{2}", dp + 1, ssCode, apiCode1));
                        }
                        else
                        {
                            TengDa.Common.Log.Logs.Info(string.Format("上料线{0},条码内容{1}提交MES校验失败，MES返回结果{2}", dp + 1, ssCode, apiCode1));
                        }
                    }
                }
            }
            else
            {
                TengDa.Common.Log.Logs.Info(string.Format("检测到{0}#上料已经断线，系统开始重新进行连接.....", dp + 1));

                Thread.Sleep(30000);
                try
                {    //重新握手连接
                    TengDa.Communication.SocketBLL.matContent[dp].Close();
                    TengDa.Communication.SocketBLL.matContent[dp] = TengDa.Communication.Panasonnic.PanasonnicSocket.PanasonnicConn(IPAddress.UPIPList[dp], 9094, 500);
                }
                catch (Exception ex)
                {
                    TengDa.Common.Log.Logs.Error(string.Format("{0}#真空炉异常详细内容:{1}", dp + 1, ex.Message));
                }
            }
        }
        /// <summary>
        /// 把报警内容解析出来
        /// </summary>
        /// <param name="i"></param>
        /// <param name="list"></param>
        /// <param name="sto"></param>
        public static void AnalyzeGetEoor(int i, List<string> list, int sto)
        {
            for (int j = 0; j < list.Count; j++)
            {
                List<TengDa.Model.BakingErrorInfo> model = TengDa.Communication.APPBLL.errorModel.Where(x => x.EWhere == list[j]).ToList();
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        string ress = string.Format(@"报警内容：{0}", item.EContent);

                        //     List <string > sss= list = application.Common.logHelp.list.Skip(m).Take(n).ToList();
                        //  if (application.Common.logHelp.list.Where(x => x.Contains(item.EContent)).ToList().Count == 0)

                        if (!TengDa.Common.logHelp.LocalLog.Contains(item.EContent))
                        {
                            if (sto == 1)
                            {
                                TengDa.Model.BakingErrorRecord model1 = new BakingErrorRecord()
                                {
                                    BBakingName = string.Format(@"{0}#Baking-A", (i + 1).ToString()),
                                    BRecordCon = ress,
                                    BRecordTime = DateTime.Now,
                                };
                                (new TengDa.DB.BakingErrorRecordDB()).Insert(model1);
                            }
                            else if (sto == 2)
                            {
                                TengDa.Model.BakingErrorRecord model1 = new BakingErrorRecord()
                                {
                                    BBakingName = string.Format(@"{0}#Baking-B", (i + 1).ToString()),
                                    BRecordCon = ress,
                                    BRecordTime = DateTime.Now,
                                };
                                (new TengDa.DB.BakingErrorRecordDB()).Insert(model1);
                            }
                            else if (sto == 3)
                            {
                                TengDa.Model.BakingErrorRecord model1 = new BakingErrorRecord()
                                {
                                    BBakingName = string.Format(@"{0}#Baking-C", (i + 1).ToString()),
                                    BRecordCon = ress,
                                    BRecordTime = DateTime.Now,
                                };
                                (new TengDa.DB.BakingErrorRecordDB()).Insert(model1);

                            }
                          TengDa.Common.Log.Logs.Info(string.Format(@"{0}#Baking炉,报警内容：{1}", (i + 1).ToString(), item.EContent));
                        }
                    }
                }
            }
        }
    }
}
