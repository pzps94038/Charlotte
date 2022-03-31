using Charlotte.DataBase.DbContextModel;
using Charlotte.Model.ManagerRole;
using Charlotte.Database.Model;
namespace Charlotte.Helper.ManagerRole
{
    public static class ManagerRoleHelper
    {
        public static async Task CreateManagerRole(ManagerRoleModel req) 
        {
            using (var db = new CharlotteContext()) 
            {
               var role = new Database.Model.ManagerRole();
               role.RoleName = req.RoleName;
               role.CreateDate = DateTime.Now;
                db.ManagerRole.Add(role);
               await db.SaveChangesAsync();
            }
        }
    }
}
