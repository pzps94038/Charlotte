using Charlotte.Model.ManagerRole;
using Charlotte.VModel.ManagerRole;

namespace Charlotte.Helper.ManagerRole
{
    public interface IManagerRoleHelper
    {
        Task CreateManagerRole(ManagerRoleModel req);
        Task<List<ManagerRoleVModel>> GetRoles();
        Task ModifyRole(int roleId, ManagerRoleModel req);
        Task DeleteRole(int roleId);
        Task BatchDeleteManagerRole(List<int> req);
    }
}
