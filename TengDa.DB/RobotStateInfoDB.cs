using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Models.Entities;

namespace TengDa.DB
{
    public class RobotStateInfoDB : RepositoryBase<RobotStateInfoEntity>
    {
        public List<RobotStateInfoEntity> GetAllData()
        {
            List<RobotStateInfoEntity> list = this.GetAll().ToList();
            return list;
        }

    }
}
