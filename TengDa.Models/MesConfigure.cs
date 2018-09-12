using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.Model;

namespace TengDa.Model
{
    /// <summary>
    /// MesConfigure:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>

    public class MesConfigure : BaseModel
    {
 
        #region Model
        private int _mesid;
        private string _inurl;
        private string _outurl;
        private string _machineurl;
        private string _account;
        private string _pwd;
        private string _insite;
        private string _inservicecode;
        private string _inresource;
        private string _outsite;
        private string _outservicecode;
        private string _outresource;
        private string _eqsite;
        private string _eqservicecode;
        private string _eqresource;
        private string _remark;
        /// <summary>
        /// id
        /// </summary>
        public int MesId
        {
            set { _mesid = value; }
            get { return _mesid; }
        }
        /// <summary>
        /// 进站url
        /// </summary>
        public string InUrl
        {
            set { _inurl = value; }
            get { return _inurl; }
        }
        /// <summary>
        /// 出站url
        /// </summary>
        public string OutUrl
        {
            set { _outurl = value; }
            get { return _outurl; }
        }
        /// <summary>
        /// 机器状态url
        /// </summary>
        public string MachineUrl
        {
            set { _machineurl = value; }
            get { return _machineurl; }
        }
        /// <summary>
        /// 账户
        /// </summary>
        public string Account
        {
            set { _account = value; }
            get { return _account; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// 入站工厂代码
        /// </summary>
        public string InSite
        {
            set { _insite = value; }
            get { return _insite; }
        }
        /// <summary>
        /// 入站接口标示
        /// </summary>
        public string InServiceCode
        {
            set { _inservicecode = value; }
            get { return _inservicecode; }
        }
        /// <summary>
        /// 入站设备编号
        /// </summary>
        public string InRESOURCE
        {
            set { _inresource = value; }
            get { return _inresource; }
        }
        /// <summary>
        /// 出站工厂代码
        /// </summary>
        public string OutSite
        {
            set { _outsite = value; }
            get { return _outsite; }
        }
        /// <summary>
        /// 出站接口标示
        /// </summary>
        public string OutServiceCode
        {
            set { _outservicecode = value; }
            get { return _outservicecode; }
        }
        /// <summary>
        /// 出站设备编号
        /// </summary>
        public string OutRESOURCE
        {
            set { _outresource = value; }
            get { return _outresource; }
        }
        /// <summary>
        /// 设备状态工厂代码
        /// </summary>
        public string EqSite
        {
            set { _eqsite = value; }
            get { return _eqsite; }
        }
        /// <summary>
        /// 设备状态接口标示
        /// </summary>
        public string EqServiceCode
        {
            set { _eqservicecode = value; }
            get { return _eqservicecode; }
        }
        /// <summary>
        /// 设备状态编号
        /// </summary>
        public string EqRESOURCE
        {
            set { _eqresource = value; }
            get { return _eqresource; }
        }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}
