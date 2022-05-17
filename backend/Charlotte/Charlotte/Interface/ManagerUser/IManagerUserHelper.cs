using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerUser;
using Charlotte.VModel.ManagerUser;

namespace Charlotte.Interface.ManagerUser
{
    public interface IManagerUserHelper: ICRUDAsyncHelper<ManagerUsersVModel, ManagerUserVModel, CreateManagerUserModel, ModifyManagerUserModel>
    {
        Task<string> ModifyManagerUserPassword(int managerUserId, ModifyManagerUserPassWordModel req);
    }
}
