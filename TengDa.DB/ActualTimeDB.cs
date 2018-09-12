using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model;

namespace TengDa.DB
{
   public  class ActualTimeDB : RepositoryBase<ActualTimeTem>
    {
        public List<ActualTimeTem> GetBakingTem(int machine, int storey)
        {
            string sql = string.Format("exec [dbo].[TemperatureAddress] '{0}','{1}'", machine+1, storey+1);
            List<ActualTimeTem> list = this.Get(sql).ToList();
            return list.OrderBy(x => x.BEstablish).ToList();
        }
    }
}
