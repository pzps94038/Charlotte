using Charlotte.DataBase.Model;
using Charlotte.Model.ManagerLogin;
using Charlotte.VModel.ManagerLogin;
using System.Data.Common;
using System.Data.SqlClient;

namespace Charlotte.Helper.ManagerLogin
{
    public interface IManagerLoginHelper
    {
        Task<(string, ManagerLoginVModel)> Login(ManagerLoginModel req);
    }
}
