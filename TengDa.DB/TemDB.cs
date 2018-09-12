using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model;

namespace TengDa.DB
{
  public  class TemDB : RepositoryBase<BakingTem>
    {
        public  List<BakingTem> QueryTemData(int mac, string sto, int side, string beginTime, string endTime)
        {
            string sql = string.Format(@"SELECT* FROM  BakingTem  WHERE BBakingName='{0}#Baking-{1}-{2}'AND BEstablish >='{3}' AND  BEstablish<='{4}'", mac + 1, sto, side + 1, beginTime, endTime);
            return this.Get(sql).ToList();
        }

    }
}
