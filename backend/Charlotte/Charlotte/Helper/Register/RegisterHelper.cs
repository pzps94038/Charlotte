using Charlotte.DataBase.DbContextModel;
using Charlotte.DataBase.Model;
using Charlotte.Interface.Register;
using Charlotte.Model.Register;
using Charlotte.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Charlotte.Helper.Register
{
    public class RegisterHelper: IRegisterHelper
    {
        public async Task<string> Register(RegisterModel req) 
        {
            string message = "";
            using (CharlotteContext db = new CharlotteContext()) 
            {
                var userMain = await db.UserMain.FirstOrDefaultAsync(a => a.Account == req.Account);
                if (userMain == null)
                {
                    UserMain data = req.Adapt<UserMain>();
                    data.Password = EncryptUtils.SHA256Encrypt(req.Password);
                    data.Flag = "Y";
                    data.CreatedDate = DateTime.Now;
                    data.Birthday = Convert.ToDateTime(req.birthday);
                    await db.UserMain.AddAsync(data);
                    await db.SaveChangesAsync();
                }
                else message = "帳號已存在";
            }
            return message;
        }
    }
}
