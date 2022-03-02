using Charlotte.DbContextModel;
using Charlotte.Model.DataBase;
using Charlotte.Model.Register;
using Mapster;

namespace Charlotte.Helper.Register
{
    public static class RegisterHelper
    {
        public static string Register(RegisterModel req) 
        {
            string message = "";
            using (CharlotteContext db = new CharlotteContext()) 
            {
                var userMain = db.UserMain.FirstOrDefault(a => a.Account == req.Account);
                if (userMain == null)
                {
                    UserMain data = req.Adapt<UserMain>();
                    data.Password = SHA256Helper.SHA256Encrypt(req.Password);
                    data.CreatedDate = DateTime.Now;
                    db.UserMain.Add(data);
                    db.SaveChanges();
                }
                else message = "帳號已存在";
            }
            return message;
        }
    }
}
