using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication
{
   public  class DataSource
    {
        /// <summary>
        /// 夹具状态
        /// </summary>
        public static List<int> fixtureList = new List<int>();

        /// <summary>
        /// 下层左温度
        /// </summary>
        public static List<string>[] temleftListA = new List<string>[9];


        /// <summary>
        /// 下层右侧温度
        /// </summary>
        public static List<string>[] temRightListA = new List<string>[9];


        /// <summary>
        /// 中层左温度
        /// </summary>
        public static List<string>[] temleftListB = new List<string>[9];

        /// <summary>
        /// 中层右侧温度
        /// </summary>
        public static List<string>[] temRightListB = new List<string>[9];



        /// <summary>
        /// 上层左温度
        /// </summary>
        public static List<string>[] temleftListC = new List<string>[9];

        /// <summary>
        /// 上层右侧温度
        /// </summary>
        public static List<string>[] temRightListC = new List<string>[9];

        /// <summary>
        /// 炉子内的夹具状态
        /// </summary>
        public static List<string>[] ChuckingStatus = new List<string>[9];


        /// <summary>
        /// 真空
        /// </summary>
        public static string[] vacListA = new string[9];

        public static string[] vacListB = new string[9];

        public static string[] vacListC = new string[9];

        /// <summary>
        /// 运行时间
        /// </summary>
        public static string[] timeListA = new string[9];

        public static string[] timeListB = new string[9];

        public static string[] timeListC = new string[9];

    }
}
