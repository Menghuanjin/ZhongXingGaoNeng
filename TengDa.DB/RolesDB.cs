using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Models.Entities;

namespace TengDa.DB
{
    public class RolesDB : RepositoryBase<RolesEntity>
    {
        public RolesEntity GetUserRolesById(int roleId)
        {
            IList<ISort> sort = new List<ISort>();
            sort.Add(new Sort { PropertyName = "RoleId", Ascending = false });
            IEnumerable<RolesEntity> list = this.GetList(new { RoleId = roleId }, sort);
            return list.FirstOrDefault();
        }
    }
}
