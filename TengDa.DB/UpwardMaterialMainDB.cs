using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model;

namespace TengDa.DB
{
    public class UpwardMaterialMainDB : RepositoryBase<UpwardMaterialMain>
    {
        private static UpwardMaterialMainDB ummd = new UpwardMaterialMainDB();
        public static UpwardMaterialMainDB UMMD
        {
            get { return ummd; }
        }

        /// <summary>
        /// //找到主表最新的夹具
        /// </summary>
        /// <param name="line"></param>
        /// <param name="electricCore"></param>
        public UpwardMaterialMain GetAllNewData(int line)
        {
            string sql = string.Format(@"SELECT TOP 1 UMID  FROM UpwardMaterialMain
                                        WHERE line='{0}'   ORDER BY UMID DESC ", line);
            List<UpwardMaterialMain> list = this.Get(sql).ToList();
            return list != null ? list[0] : null;
        }

    }
}
