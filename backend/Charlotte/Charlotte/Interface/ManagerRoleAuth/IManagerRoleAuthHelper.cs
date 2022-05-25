using Charlotte.Model.ManagerRoleAuth;
using Charlotte.VModel.ManagerRoleRouter;

namespace Charlotte.Interface.ManagerRoleAuth
{
    public interface IManagerRoleAuthHelper
    {
        Task<List<ManagerRoleAuthVModel>> GetManagerRoleRouters(int roleId);
        Task CreateOrUpdateRoleAuth(int roleId, List<ManagerRoleAuthModel> req);
        Task<CheckManagerRoleAuthVModel<bool>> CheckRoleAuth(int userId, string routerPath);
    }
}
