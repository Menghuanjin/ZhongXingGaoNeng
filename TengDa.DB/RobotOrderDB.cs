using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model;
using TengDa.Models.Entities;

namespace TengDa.DB
{
  public  class RobotOrderDB : RepositoryBase<RobotEntity>
    {
        public static RobotOrderDB rodb = new RobotOrderDB();
        public static RobotOrderDB RODB
        {
            get { return rodb; }
        }
        /// <summary>
        /// 将上料工位完成的夹具找到空的baking炉
        /// </summary>
        /// <returns></returns>
        public RobotEntity GetAllDataNull()
        {
            string sql = string.Format(@"exec [P_GetBakingAddress] 1,0");
            List<RobotEntity> list = this.Get<RobotEntity>(sql).ToList();
            return list[0];
        }
        ///// <summary>
        ///// 查询缓存位夹具Code
        ///// </summary>
        ///// <param name="unm"></param>
        ///// <param name="Code">-10为取-20为放</param>
        ///// <returns></returns>
        //public int GetAllDataCache(int num, int Code)
        //{
        //    string sql = string.Format(@"SELECT ffd.TakeCode ,ffd.ReleaseCode  FROM FixtureFurnaceMain ffm LEFT JOIN FixtureFurnaceDetail ffd ON
        //ffm.FFMId= ffd.FFmId
        //WHERE ffm.FFMCache=20 AND ffd.FFDNumber={0}
        //ORDER BY FFM.FFMId,FFDNumber ", num);
        //    List<RobotEntity> list = this.Get<RobotEntity>(sql).ToList();
        //    return Code == 10 ? (int)list[0].TakeCode : (int)list[0].ReleaseCode;
        //}

        /// <summary>        
        /// 找到缓存位夹具   
        /// </summary> 
        ///                --10：上料台1(3#线)
        ///                --15：上料台2(4#线)
        ///                --20：缓存台(3#线)
        /// <param name="cac">唯一标示类型</param>

        /// <param name="num">层数</param>
        /// <param name="Code">10为取-20为放</param>
        /// <returns></returns>
        public int GetAllDataNum(int cac, int num, int Code)
        {
            string sql = string.Format(@"
SELECT ffd.TakeCode ,ffd.ReleaseCode  FROM FixtureFurnaceMain ffm LEFT JOIN FixtureFurnaceDetail ffd ON
ffm.FFMId= ffd.FFmId
WHERE  ffm.FFMCache={0} AND ffd.FFDNumber={1}
ORDER BY FFM.FFMId,FFDNumber  ", cac, num);
            List<RobotEntity> list = this.Get<RobotEntity>(sql).ToList();
            return Code == 10 ? Convert.ToInt16(list[0].TakeCode) : Convert.ToInt16(list[0].ReleaseCode);
        }
    }
}
