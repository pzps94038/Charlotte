using Charlotte.Model.ManagerUser;
using Charlotte.VModel.ManagerUser;

namespace Charlotte.Helper.ManagerUser
{
    public interface IManagerUserHelper
    {
        Task CreateManagerUser(CreateManagerUserModel req);
        Task<ManagerUserVModel> GetManagerUser(int managerUserId);
        Task<List<ManagerUsersVModel>> GetManagerUsers();
        Task ModifyManagerUser(int managerUserId, ModifyManagerUserModel req);
        Task DeleteManagerUser(int managerUserId);
        Task BatchDeleteManagerUser(List<int> req);
        Task<string> ModifyManagerUserPassword(int managerUserId, ModifyManagerUserPassWordModel req);
    }
}
