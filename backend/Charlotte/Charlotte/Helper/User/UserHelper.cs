using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Enum;
using Charlotte.Interface.ManagerUser;
using Charlotte.Model.ManagerUser;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Charlotte.VModel.Shared;
using Dapper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Charlotte.CustomizeException.CustomizeException;
using System.Linq.Dynamic.Core;
using Charlotte.Interface.Shared;
using Charlotte.Interface.User;
using Charlotte.Model.User;
using Charlotte.VModel.User;

namespace Charlotte.Helper.UserHelper
{
    public class UserHelper : IUserHelper
    {

        public async Task CreateAsync(CreateUserModel request)
        {
            using (var db = new CharlotteContext())
            {
                var user = new UserMain();
                if (request.Address != null) 
                    user.Address = request.Address;
                user.UserName = request.UserName;
                user.Account = request.Account;
                user.Email = request.Email;
                user.Flag = request.Flag ? "Y" : "N";
                user.CreatedDate = DateTime.Now;
                user.Birthday = request.Birthday;
                user.Password = EncryptUtils.SHA256Encrypt(request.Password);
                db.UserMain.Add(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task ModifyAsync(int id, ModifyUserModel request)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.UserMain.SingleAsync(a => a.UserId == id);
                if (request.UserName != null)
                    data.UserName = request.UserName;
                if (request.Account != null)
                    data.Account = request.Account;
                if (request.password != null && data.Password != request.password)
                    data.Password = EncryptUtils.SHA256Encrypt(request.password);
                if (request.Email != null)
                    data.Email = request.Email;
                if (request.Address != null)
                    data.Address = request.Address;
                if (request.Birthday != null)
                    data.Birthday = (DateTime)request.Birthday;
                if (request.Flag != null)
                    data.Flag = (bool)request.Flag ? "Y" : "N";
                data.ModifyDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new CharlotteContext())
            {
                var data = await db.UserMain.SingleAsync(a => a.UserId == id);
                db.UserMain.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task BatchDeleteAsync(List<int> idList)
        {
            using (var db = new CharlotteContext())
            {
                var datas = await db.UserMain.Where(a => idList.Contains(a.UserId)).ToListAsync();
                db.UserMain.RemoveRange(datas);
                await db.SaveChangesAsync();
            }
        }

        public async Task<TableVModel<UsersVModel>> GetAllAsync(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            using (var db = new CharlotteContext())
            {
                var query = db.UserMain.Select(a=> new { a.UserId, a.UserName, a.Account, a.Password, a.Address, a.Email, Birthday = a.Birthday.ToString("yyyy-MM-dd"), a.Flag }).AsQueryable();
                if (filterStr != null)
                {
                    query = query.Where(a =>  a.UserName.Contains(filterStr) ||                    
                                              a.Email.Contains(filterStr) ||
                                              a.Flag.Contains(filterStr) ||
                                              (a.Address != null && a.Address.Contains(filterStr)) ||
                                              a.UserId.ToString().Contains(filterStr)
                                        );
                };
                var tableTotalCount = query.Count();
                if (orderBy != null && orderDescription != null)
                    query = orderDescription == "desc" ? query.OrderBy($"{orderBy} desc") : query.OrderBy($"{orderBy} asc");
                if (limit != null && offset != null)
                    query = query.Skip((int)offset).Take((int)limit);
                var result = await query.ToListAsync();
                return new TableVModel<UsersVModel>(result.Adapt<List<UsersVModel>>(), tableTotalCount) ;
            }
        }
    }
}
