using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model.Entities;

namespace TengDa.DB
{
    public class UsersDB:RepositoryBase<UsersEntity>
    {
        public List<UsersEntity> GetAllData()
        {
            IEnumerable<UsersEntity> list = this.GetAll();
            return list.ToList();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userNumber"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public UsersEntity GetUserLogin(string userNumber,string userPwd)
        {
            IList<ISort> sort = new List<ISort>();
            sort.Add(new Sort { PropertyName = "UserId", Ascending = false });
            IEnumerable<UsersEntity> list = this.GetList(new { UserNumber = userNumber, UserPwd = userPwd }, sort);
            return list.FirstOrDefault();
        }
    }
}
