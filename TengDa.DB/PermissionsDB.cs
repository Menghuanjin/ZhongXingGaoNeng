using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Models.Entities;

namespace TengDa.DB
{
    public class PermissionsDB : RepositoryBase<PermissionsEntity>
    {
        public List<PermissionsEntity> GetAllData()
        {
            IEnumerable<PermissionsEntity> list = this.GetAll();
            return list.ToList();
        }
    }
}
