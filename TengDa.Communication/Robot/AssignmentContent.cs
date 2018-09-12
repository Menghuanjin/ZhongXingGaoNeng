using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication.Robot
{   
    /// <summary>
    /// 执行任务内容
    /// </summary>
    public class AssignmentContent
    {
        /// <summary>
        /// 执行任务名字
        /// </summary>
        public string assignmentName { get; set; }
        /// <summary>
        /// 任务类型(上料1/下料2)
        /// </summary>
        public int assignmentType { get; set; }
        /// <summary>
        /// 任务等级
        /// </summary>
        public int assignmentGrade { get; set; }
        /// <summary>
        /// 开始地方
        /// </summary>
        public int beginLocal { get; set; }
        /// <summary>
        /// 结束地方
        /// </summary>
        public int endLocal { get; set; }

        /// <summary>
        /// 夹具条码
        /// </summary>
        public string chuckingBarCode { get; set; }
        /// <summary>
        /// 夹具信息
        /// </summary>
        public string chuckingInfo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime foundTime { get; set; }

    }
}
