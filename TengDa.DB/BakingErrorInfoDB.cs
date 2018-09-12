using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model;

namespace TengDa.DB
{
   public class BakingErrorInfoDB : RepositoryBase<BakingErrorInfo>
    {
        private static BakingErrorInfoDB beif = new BakingErrorInfoDB();
        public static BakingErrorInfoDB BEIF
        {
            get { return beif; }
        }
        public List<BakingErrorInfo> GetAllData()
        {
            IEnumerable<BakingErrorInfo> list = this.GetAll();
            return list.ToList();
        }
    }
}
