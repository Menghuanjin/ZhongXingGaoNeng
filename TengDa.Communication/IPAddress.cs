using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TengDa.Communication
{
  public  class IPAddress
    {
        /// <summary>
        /// 上料
        /// </summary>
        public static string[] UPIPList = new string[]
        {
            IPAddress.UP1IP,
                 IPAddress.UP2IP
         };
        /// <summary>
        /// baking
        /// </summary>
        public static string[] bakIPlist = new string[]
         {
             IPAddress.Baking1IP,
                 IPAddress.Baking2IP,
                      IPAddress.Baking3IP,
                         IPAddress.Baking4IP,
                             IPAddress.Baking5IP,
                                 IPAddress.Baking6IP,
                IPAddress.Baking7IP,   IPAddress.Baking8IP,
                   IPAddress.Baking9IP
         };
        private static string _baking1IP;
        /// <summary>
        /// baking1
        /// </summary>
        public static string Baking1IP
        {
            get {

                if (string.IsNullOrEmpty(_baking1IP))
                {
                    _baking1IP= System.Configuration.ConfigurationManager.ConnectionStrings["baking1IP"].ToString();
                }
                return _baking1IP;
            }
        }
        private static string _baking2IP;

        public static string Baking2IP
        {
            get {

                if (string.IsNullOrEmpty(_baking2IP))
                {
                    _baking2IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking2IP"].ToString();
                }
                return _baking2IP;
            }
        }
        private static string _baking3IP;

        public static string Baking3IP
        {
            get {

                if (string.IsNullOrEmpty(_baking3IP))
                {
                    _baking3IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking3IP"].ToString();
                }
                return _baking3IP;
            }

        }
        private static string _baking4IP;
        public static string Baking4IP
        {
            get
            {
                if (string.IsNullOrEmpty(_baking4IP))
                {
                    _baking4IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking4IP"].ToString();
                }
                return _baking4IP;
            }
        }
        private static string _baking5IP;

        public static string Baking5IP
        {
            get {

                if (string.IsNullOrEmpty(_baking5IP))
                {
                    _baking5IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking5IP"].ToString();
                }
                return _baking5IP;

            }
        }
        private static string _baking6IP;
        public static string Baking6IP
        {
            get
            {
                if (string.IsNullOrEmpty(_baking6IP))
                {
                    _baking6IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking6IP"].ToString();
                }
                return _baking6IP;
            }

        }
        private static string _baking7IP;

        public static string Baking7IP
        {
            get
            {
                if (string.IsNullOrEmpty(_baking7IP))
                {
                    _baking7IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking7IP"].ToString();
                }
                return _baking7IP;
            }
        }
        private static string _baking8IP;
        public static string Baking8IP
        {
            get
            {
                if (string.IsNullOrEmpty(_baking8IP))
                {
                    _baking8IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking8IP"].ToString();
                }
                return _baking8IP;
            }
        }
        private static string _baking9IP;

        public static string Baking9IP
        {
            get
            {
                if (string.IsNullOrEmpty(_baking9IP))
                {
                    _baking9IP = System.Configuration.ConfigurationManager.ConnectionStrings["baking9IP"].ToString();
                }
                return _baking9IP;
            }

        }
        private static string _transportRobot;
        /// <summary>
        /// 搬运机器人
        /// </summary>
        public static string transportRobot
        {
            get
            {
                if (string.IsNullOrEmpty(_transportRobot))
                {
                    _transportRobot = System.Configuration.ConfigurationManager.ConnectionStrings["transportRobot"].ToString();
                }

                return _transportRobot;
            }
        }



        private static string _leftRobot;
        /// <summary>
        /// 左边机器人
        /// </summary>
        public static string leftRobot
        {
            get
            {
                if (string.IsNullOrEmpty(_leftRobot))
                {
                    _leftRobot = System.Configuration.ConfigurationManager.ConnectionStrings["leftRobot"].ToString();
                }

                return _leftRobot;
            }
        }
        /// <summary>
        /// 右边机器人
        /// </summary>
        private static string _rightRobot;

        public static string rightRobot
        {
            get
            {
                if (string.IsNullOrEmpty(_rightRobot))
                {
                    _rightRobot = System.Configuration.ConfigurationManager.ConnectionStrings["rightRobot"].ToString();
                }

                return _rightRobot;
            }
        }
        private static string _UP1IP;
        /// <summary>
        /// 上料1
        /// </summary>
        public static string UP1IP
        {
            get {
                if (string.IsNullOrEmpty(_UP1IP))
                {
                    _UP1IP = System.Configuration.ConfigurationManager.ConnectionStrings["UP1IP"].ToString();
                }
                return _UP1IP;

            }
        }
        private static string _UP2IP;
        /// <summary>
        /// 上料2
        /// </summary>
        public static string UP2IP
        {
            get
            {
                if (string.IsNullOrEmpty(_UP2IP))
                {
                    _UP2IP = System.Configuration.ConfigurationManager.ConnectionStrings["UP2IP"].ToString();
                }
                return _UP2IP;
            }
        }
        private static string _blanking1IP;
        /// <summary>
        /// 下料1
        /// </summary>
        public static string blanking1IP
        {
            get
            {
                if (string.IsNullOrEmpty(_blanking1IP))
                {
                    _blanking1IP = System.Configuration.ConfigurationManager.ConnectionStrings["blanking1IP"].ToString();
                }
                return _blanking1IP;
            }
        }
        private static string _blanking2IP;
        /// <summary>
        /// 下料2
        /// </summary>
        /// 

        public static string blanking2IP
        {
            get
            {
                if (string.IsNullOrEmpty(_blanking2IP))
                {
                    _blanking2IP = System.Configuration.ConfigurationManager.ConnectionStrings["blanking2IP"].ToString();
                }
                return _blanking2IP;
            }
        }
        private static string _sweepCode1IP;
        /// <summary>
        /// 扫码枪1
        /// </summary>
        public static string SweepCode1IP
        {
            get
            {
                if (string.IsNullOrEmpty(_sweepCode1IP))
                {
                    _sweepCode1IP = System.Configuration.ConfigurationManager.ConnectionStrings["sweepCode1IP"].ToString();
                }
                return _sweepCode1IP;
            }
        }

        private static string _sweepCode2IP;

        public static string SweepCode2IP
        {
            get {
                if (string.IsNullOrEmpty(_sweepCode2IP))
                {
                    _sweepCode2IP = System.Configuration.ConfigurationManager.ConnectionStrings["sweepCode2IP"].ToString();
                }
                return _sweepCode2IP;
            }
        }
        private static string _sweepCode3IP;

        public static string SweepCode3IP
        {
            get
            {
                if (string.IsNullOrEmpty(_sweepCode3IP))
                {
                    _sweepCode3IP = System.Configuration.ConfigurationManager.ConnectionStrings["sweepCode3IP"].ToString();
                }
                return _sweepCode3IP;

            }
        }
        private static string _sweepCode4IP;
        public static string SweepCode4IP
        {
            get {
                if (string.IsNullOrEmpty(_sweepCode4IP))
                {
                    _sweepCode4IP = System.Configuration.ConfigurationManager.ConnectionStrings["sweepCode4IP"].ToString();
                }
                return _sweepCode4IP;

            }
        }
        private static string _sweepCode5IP;

        public static string SweepCode5IP
        {
            get
            {

                if (string.IsNullOrEmpty(_sweepCode5IP))
                {
                    _sweepCode5IP = System.Configuration.ConfigurationManager.ConnectionStrings["sweepCode5IP"].ToString();
                }
                return _sweepCode5IP;
            }
        }
        private static string _sweepCode6IP;

        public static string SweepCode6IP
        {
            get {

                if (string.IsNullOrEmpty(_sweepCode6IP))
                {
                    _sweepCode6IP = System.Configuration.ConfigurationManager.ConnectionStrings["sweepCode6IP"].ToString();
                }
                return _sweepCode6IP;
            }
        }
    }
}
