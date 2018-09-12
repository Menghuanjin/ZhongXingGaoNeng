using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model;
using TengDa.Models;

namespace TengDa.DB
{
  public  class MesConfigureDB : RepositoryBase<MesConfigure>
    {
        public List<MesConfigure> GetAllData()
        {
            IEnumerable<MesConfigure> list = this.GetAll().ToList();
            return list.ToList();
        }
        public MesConfigure GetUserRolesById(int MesId = 1)
        {
            IList<ISort> sort = new List<ISort>();
            sort.Add(new Sort { PropertyName = "MesId", Ascending = false });
            IEnumerable<MesConfigure> list = this.GetList(new { MesId = MesId }, sort);
            return list.FirstOrDefault();
        }
    }
}
