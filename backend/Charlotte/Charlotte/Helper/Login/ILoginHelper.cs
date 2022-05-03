using Charlotte.DataBase.Model;
using Charlotte.Model;
using Charlotte.Model.Login;
using System.Data.Common;
using System.Data.SqlClient;

namespace Charlotte.Helper.Login
{
    public interface ILoginHelper
    {
        Task<(string, Token)> Login(LoginModel req);
    }
}
