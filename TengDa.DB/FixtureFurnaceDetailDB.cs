using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Models.Entities;
using TengDa.DataAccess;
using TengDa.Models.ViewModel;

namespace TengDa.DB
{
    public class FixtureFurnaceDetailDB : RepositoryBase<FixtureFurnaceDetailEntity>
    {
        #region init dbconnection
        //private static readonly string connString = ConfigurationManager.ConnectionStrings["PharmacySystem"].ConnectionString;
        //private IDbConnection _conn;
        //public IDbConnection Conn
        //{
        //    get
        //    {
        //        _conn = new SqlConnection(connString);
        //        _conn.Open();
        //        return _conn;
        //    }
        //}
        #endregion
        public List<FixtureFurnaceDetaiViewModel> GetAllData()
        { 
            string sql = string.Format(@"  select ffd.*,cai.CABarCode,cai.CAState,
  ffm.FFMState, ffm.FFMNumber, ffm.FFMName, ffm.FFMCountLayer,
  ffm.FFMBerthPosition
   from FixtureFurnaceDetail ffd
  left   join FixtureFurnaceMain ffm
   on ffm.FFMId = ffd.FFMId left  join ChuckingApplianceInfo cai
on cai.CAId = ffd.CAId");
            List<FixtureFurnaceDetaiViewModel> list = this.Get<FixtureFurnaceDetaiViewModel>(sql).ToList();
            return list.ToList();
        }
        public List<FixtureFurnaceDetaiViewModel> GetAllDataByFFDID(int FFDID)
        {
            string sql = string.Format(@"  select ffd.*,cai.CABarCode,cai.CAState,
  ffm.FFMState, ffm.FFMNumber, ffm.FFMName, ffm.FFMCountLayer,
  ffm.FFMBerthPosition
   from FixtureFurnaceDetail ffd
  left   join FixtureFurnaceMain ffm
   on ffm.FFMId = ffd.FFMId left  join ChuckingApplianceInfo cai
on cai.CAId = ffd.CAId where ffd.FFMId={0}  
order by ffd.FFDNumber", FFDID);
            List<FixtureFurnaceDetaiViewModel> list = this.Get<FixtureFurnaceDetaiViewModel>(sql).ToList();
            return list.ToList();
        }
        /// <summary>
        /// 查询所有没有入炉子机机台
        /// </summary>
        /// <returns></returns>
        public int?  RobotAccomplish()
        {
            string sql = string.Format(@"  select ffd.*,cai.CABarCode,cai.CAState,
  ffm.FFMState, ffm.FFMNumber, ffm.FFMName, ffm.FFMCountLayer,
  ffm.FFMBerthPosition
   from FixtureFurnaceDetail ffd
  left   join FixtureFurnaceMain ffm
   on ffm.FFMId = ffd.FFMId left  join ChuckingApplianceInfo cai
on cai.CAId = ffd.CAId 
WHERE FFM.FFMId IN (2,3,4,5,6,10,11,12,13,14) and ffd.FFDState=10 
ORDER BY FFM.FFMId,FFDNumber");
            List<FixtureFurnaceDetaiViewModel> list = this.Get<FixtureFurnaceDetaiViewModel>(sql).ToList();
            return list[0].ReleaseCode;
        }
    }
}
