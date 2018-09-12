using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model;

namespace TengDa.DB
{
   public  class BakingErrorRecordDB : RepositoryBase<BakingErrorRecord>
    {

        public List<BakingErrorRecord> QueryErrorInfo(int mac, string sto, string beginTime, string endTime)
        {
            string sql = string.Format(@"SELECT  * FROM BakingErrorRecord WHERE BBakingName='{0}#Baking-{1}'AND   BRecordTime>='{2}'AND BRecordTime<='{3}'", mac + 1, sto, beginTime, endTime);
            return this.Get(sql).ToList();
        }
    }
}
