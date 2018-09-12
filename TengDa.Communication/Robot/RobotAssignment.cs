using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TengDa.Communication.Robot
{
  public class RobotAssignment
    {
      private static List<TengDa.Model.RobotAssignment> AssignmentList = new List<TengDa.Model.RobotAssignment>();
        /// <summary>
        /// 加入任务集合
        /// </summary>
        /// <param name="ac"></param>
        private static void AddAssignment(TengDa.Model.RobotAssignment ac)
        {
            AssignmentList.Add(ac);
        }
        /// <summary>
        /// 任务删除
        /// </summary>
        public static void DeleteAssignment(TengDa.Model.RobotAssignment ac)
        {
            AssignmentList.Remove(ac);
          //  AssignmentList.Dequeue();
        }
        /// <summary>
        /// 任务创建
        /// </summary>
        public static void AssignmentFound()
        {
 
            while (true)
            {
               

            }

        }

        /// <summary>
        /// 机器人执行任务
        /// </summary>
        public static void ExecuteTheTask()
        {
            while (true)
            {
                if (TengDa.Communication.Robot.RobotAssignment.AssignmentList.Count != 0)
                {
                    int ss = TengDa.Communication.Robot.RobotAssignment.AssignmentList.Max(x =>(int)x.RAGrade);//找到等级最高的任务先执行

                    List<TengDa.Model.RobotAssignment> acexe = TengDa.Communication.Robot.RobotAssignment.AssignmentList.Where(x => (int)x.RAGrade == ss).ToList();

                    AnalyzeAssignment(acexe[0]);

                }
                else
                {
                    Thread.Sleep(20000);
                }
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// 机器人解析任务
        /// </summary>
        /// <param name="axexe"></param>
        private static void AnalyzeAssignment(TengDa.Model.RobotAssignment axexe)
        {

            TengDa.Communication.Robot.RobotAssignment.DeleteAssignment(axexe);
        }
    }
}
