using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 机器人实体类
/// </summary>
namespace TengDa.Model
{
   public  class RobotEntity
    {
        private int? _releasecode;

        public int? ReleaseCode
        {
            get { return _releasecode; }
            set { _releasecode = value; }
        }
        private int? _takecode;

        public int? TakeCode
        {
            get { return _takecode; }
            set { _takecode = value; }
        }
        private int? _ipaddress;

        public int? IpAddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
        private int? _ffdLayer;

        public int? FFDLayer
        {
            get { return _ffdLayer; }
            set { _ffdLayer = value; }
        }

    }
}
