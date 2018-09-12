using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model.UM;

namespace TengDa.DB
{
  public  class UpwardMaterialDetailDB : RepositoryBase<UpwardMaterialDetail>
    {
        private static UpwardMaterialDetailDB umd = new UpwardMaterialDetailDB();
        public static UpwardMaterialDetailDB UMD
        {
            get { return umd; }
        }

    }
}
