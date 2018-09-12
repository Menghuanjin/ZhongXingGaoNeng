using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Models.Common
{
    public  class SysEnum
    {
    }
    /// <summary>
    /// 机台停靠位置
    /// </summary>
    public enum BerthPositionType
    {
        [Description("顶部")]
        Top=1,
        [Description("底部")]
        Bottom=2,
    }

    /// <summary>
    /// PLC状态
    /// </summary>
    public enum PLCStateType
    {
        [Description("生产中")]
        Working = 0,
        [Description("待机中")]
        Standby = 1,
        [Description("报警中")]
        Alarm = 2,
    }

    /// <summary>
    /// 是否可用
    /// </summary>
    public enum YesOrNoType
    {
        [Description("是")]
        Yes=1,
        [Description("否")]
        No=0,
    }
    /// <summary>
    /// 夹具炉炉腔状态
    /// </summary>
    public enum FFDState
    {
        [Description("无夹具开门")]
        NoFixtureOpen = 10,
        [Description("无夹具关门")]
        NoFixtureClose = 20,
        [Description("有夹具开门")]
        HaveFixtureOpen = 30,
        [Description("有夹具关门")]
        HaveFixtureClose = 40,
    }

    /// <summary>
    /// 监控界面生成控件类型
    /// </summary>
    public enum CreateControlType
    {
        [Description("炉子")]
        Stove = 1,
        [Description("上下料平台")]
        Platform = 2,       
    }
}
