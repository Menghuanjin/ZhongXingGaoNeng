using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TengDa.DB.Base;
using TengDa.Model.Entities;

namespace TengDa.DB
{
    public class TestDb : RepositoryBase<UsersEntity>
    {


        //    /// <summary>
        //    /// Dapper 原生用法
        //    /// </summary>
        //    public void DapperDemo()
        //    {

        //        //做为Demo以下语法的支持，  声明的一个变量
        //        IDbConnection connDemo = this.DBSession.Connection;



        //        IEnumerable<UsersEntity> listDemo = connDemo.Query<UsersEntity>("SELECT * FROM Users AS u  ");

        //        IEnumerable<UsersEntity> listDemo1 =
        //        connDemo.Query<UsersEntity>("SELECT * FROM Users AS u WHERE u.UserId=@UserId AND u.LoginName LIKE @LoginName",
        //                                      new { UserId = 11, LoginName = "%王老五%" });



        //        IEnumerable<UsersEntity> listDemo2 = connDemo.Query<UsersEntity>("SELECT * FROM Users AS u WHERE u.UserId IN @UserIds ",
        //            new { UserIds = new int[] { 1, 2, 3 }.AsEnumerable() });



        //        string sqlDemo = @"SELECT * FROM  Users AS u
        //                                   LEFT JOIN UserInfo  AS ui
        //                                       ON  u.UserId = ui.UserId";
        //        IEnumerable<UsersModel> listDemo3 = connDemo.Query<UsersModel, UserInfoEntity, UsersModel>
        //            (sqlDemo, (user, userinfo) =>
        //            {
        //                user.UserInfo = userinfo; return user;
        //            }).ToList();


        //        string sqlDemo1 = @"SELECT * FROM Users AS u WHERE u.UserId=@UserId 
        //                            SELECT * FROM UserInfo AS ui WHERE ui.UserId=@UserId";
        //        using (var multi = connDemo.QueryMultiple(sqlDemo1, new { UserId = 1 }))
        //        {
        //            var user = multi.Read<UsersEntity>().Single();
        //            var userinfo = multi.Read<UserInfoEntity>().Single();
        //        }


        //        connDemo.Execute("sql 语句");


        //        //connDemo.Execute("insert into  users u values (@p1,@p2)", new { p1 = 123, p2 = "123sedf" });


        //        //存储过程
        //        var UsersEntity = connDemo.Query<UsersEntity>("spGetUser", new { Id = 1 }, commandType: CommandType.StoredProcedure).SingleOrDefault();



        //    }




        //    /// <summary>
        //    /// DapperExtensions 用法
        //    /// </summary>
        //    public void DapperExtensions()
        //    {
        //        //实体类
        //        UsersEntity entity = new UsersEntity();


        //        IDbTransaction tran = this.DBSession.Begin();
        //        int userId = this.Insert(entity, tran);//插入
        //        int userId1 = this.Insert(entity, tran);//插入 
        //        tran.Commit();






        //        bool isSuccess = this.Update(entity);//更新






        //        int count = this.Delete(entity);//删除

        //        entity = this.GetById(1);//获得实体


        //        int count1 = this.Count(new { ID = 1 });  //数量

        //        //查询所有
        //        IEnumerable<UsersEntity> list = this.GetAll();


        //        //条件查询
        //        IList<ISort> sort = new List<ISort>();
        //        sort.Add(new Sort { PropertyName = "UserId", Ascending = false });
        //        IEnumerable<UsersEntity> list1 = this.GetList(new { UserId = 1, Name = "123" }, sort);


        //        //orm 拼接条件 查询         繁琐 不灵活      不太好用
        //        IList<IPredicate> predList = new List<IPredicate>();
        //        predList.Add(Predicates.Field<UsersEntity>(p => p.LoginName, Operator.Like, "不知道%"));
        //        predList.Add(Predicates.Field<UsersEntity>(p => p.UserId, Operator.Eq, 1));
        //        IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());
        //        list = this.GetList(predGroup);


        //        //分页查询         
        //        long allRowsCount = 0;
        //        this.GetPageList(1, 10, out allRowsCount, new { ID = 1 }, sort);

        //    }




        //    public void DapperExtensionsLambda()
        //    {
        //        //SELECT [Users].[UserId],
        //        //       [Users].[LoginName],
        //        //       [Users].[Password]
        //        //FROM   [Users]
        //        //WHERE  [Users].[LoginName] = @LoginName_1
        //        var fromDemo = this.LambdaQuery()
        //                           .Select(p => new { p.UserId, p.LoginName, p.Password })   //不支持As 
        //                           .Where(p => p.LoginName == "很好很强大");


        //        //支持 返回  DataReader、DataSet、DataTable、 泛型集合  
        //        fromDemo.ToDataReader();
        //        fromDemo.ToDataSet();
        //        fromDemo.ToDataTable();
        //        IEnumerable<UsersModel> list = fromDemo.ToList<UsersModel>();



        //        var select = new Select<UsersEntity>();
        //        select.AddSelect(p => p.Remark.As("Remark"));   //Expression<Func<T, bool>>类型支持     as 语法
        //        fromDemo = this.LambdaQuery().AddSelect(select);



        //        //      SELECT [Users].[UserId],
        //        //       [Users].[LoginName],
        //        //       [Users].[Password],
        //        //       [Users].[Status],
        //        //       [Users].[CreateTime],
        //        //       [Users].[UpdateTime],
        //        //       [Users].[Remark]
        //        //FROM   [Users]
        //        //WHERE  (
        //        //           (
        //        //               ([Users].[LoginName] LIKE @LoginName_1)
        //        //               AND ([Users].[Status] NOT IN (@Status_2, @Status_3, @Status_4))
        //        //           )
        //        //           AND ([Users].[CreateTime] >= @CreateTime_5)
        //        //       )
        //        //       AND ([Users].[UpdateTime] IS NOT NULL)
        //        var fromDemo2 = this.LambdaQuery()
        //                          .Where(p => p.LoginName.Like("%王老五%")                          //like
        //                                      && p.Status.NotIn<string>("1", "2", "3")                     //in  or  not in
        //                                      && p.CreateTime >= Convert.ToDateTime("2016-01-21")          //时间比较
        //                                      && p.UpdateTime.IsNotNull()                         //is null
        //                           );



        //        //SELECT [Users].[UserId],
        //        //       [Users].[LoginName],
        //        //       [Users].[Password],
        //        //       [Users].[Status],
        //        //       [Users].[CreateTime],
        //        //       [Users].[UpdateTime],
        //        //       [Users].[Remark]
        //        //FROM   [Users]
        //        //WHERE  (
        //        //           (
        //        //               ([Users].[LoginName] LIKE @LoginName_1)
        //        //               AND ([Users].[Status] = @Status_2)
        //        //           )
        //        //           OR ([Users].[UserId] = @UserId_3)
        //        //       )
        //        var where = new Where<UsersEntity>();
        //        where.And(p => p.LoginName.Like("%李二蛋%") && p.Status == 1);
        //        where.Or(p => p.UserId == 1);
        //        var fromDemo3 = this.LambdaQuery().Where(where);



        //        //SELECT [Users].[UserId],
        //        //       [Users].[LoginName],
        //        //       [Users].[Password],
        //        //       [Users].[Status],
        //        //       [Users].[CreateTime],
        //        //       [Users].[UpdateTime],
        //        //       [Users].[Remark]
        //        //FROM   [Users]
        //        //ORDER BY
        //        //       CreateTime  ASC,
        //        //       UpdateTime  ASC,
        //        //       UserId         DESC 
        //        var fromDemo4 = this.LambdaQuery()
        //            .OrderBy(p => new { p.CreateTime, p.UpdateTime })
        //            .AddOrderByDescending(p => new { p.UserId });



        //        //"SELECT * FROM [Users] INNER JOIN UserInfo ON ([Users].[UserId] = [UserInfo].[UserId]) "
        //        var fromDemo5 = this.LambdaQuery()
        //            .InnerJoin<UserInfoEntity>((u, ui) => u.UserId == ui.UserId);



        //        //SELECT DISTINCT TOP 100 * 
        //        //FROM   [Users] WITH (NOLOCK) INNER
        //        //       JOIN UserInfo WITH (NOLOCK)
        //        //            ON  ([Users].[UserId] = [UserInfo].[UserId]) 
        //        var fromDemo6 = this.LambdaQuery()
        //                        .InnerJoin<UserInfoEntity>((u, ui) => u.UserId == ui.UserId).
        //                        WithNoLock().
        //                        Top(100)
        //                        .Distinct();



        //        //SELECT TOP(10) [_proj].* 
        //        //FROM   (
        //        //           SELECT ROW_NUMBER() OVER(ORDER BY CURRENT_TIMESTAMP) AS [_row_number],
        //        //                  *
        //        //           FROM   [Users] WITH (NOLOCK) LEFT
        //        //                  JOIN UserInfo WITH (NOLOCK)
        //        //                       ON  ([Users].[UserId] = [UserInfo].[UserId])
        //        //           WHERE  ([Users].[UserId] > @UserId_1)
        //        //                  AND ([UserInfo].[Sex] = @Sex_2)
        //        //       ) [_proj]
        //        //WHERE  [_proj].[_row_number] >= @_pageStartRow
        //        //ORDER BY
        //        //       [_proj].[_row_number]
        //        var fromDemo7 = this.LambdaQuery()
        //                             .LeftJoin<UserInfoEntity>((u, ui) => u.UserId == ui.UserId).WithNoLock()
        //                             .Where<UserInfoEntity>((u, ui) => u.UserId > 1 && ui.Sex == 1)
        //                             .Page(2, 10);



        //        //DELETE 
        //        //FROM   [Users]
        //        //WHERE  ([Users].[Status] = @Status_1)
        //        //       AND (
        //        //[Users].[CreateTime] > @CreateTime_2
        //        var deleteDemo = this.LambdaDelete()
        //                        .Where(p => p.Status == 1 && p.CreateTime > DateTime.Now);


        //        //UPDATE [Users]
        //        //SET    [Users].[Remark] = @Remark_3
        //        //WHERE  [Users].[Status] = @Status_4
        //        var updateDemo = this.LambdaUpdate()
        //                             .Set(p => p.Remark == "Remark")
        //                             .Where(p => p.Status == 1);

        //    }
        //}
    }
}
