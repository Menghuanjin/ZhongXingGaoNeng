using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model.Entities;
using TengDa.Models.Entities;

namespace TengDa.DB
{
    public class FixtureFurnaceMainDB : RepositoryBase<FixtureFurnaceMainEntity>
    {
        public List<FixtureFurnaceMainEntity> GetAllData()
        {
            IEnumerable<FixtureFurnaceMainEntity> list = this.GetAll().ToList();
            return list.ToList();
        }
    }
}
