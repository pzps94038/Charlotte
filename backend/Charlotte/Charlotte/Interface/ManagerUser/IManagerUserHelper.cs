using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerUser;
using Charlotte.VModel.ManagerUser;
using Charlotte.VModel.Shared;

namespace Charlotte.Interface.ManagerUser
{
    public interface IManagerUserHelper: ICRUDAsyncHelper<TableVModel<ManagerUsersVModel>, ManagerUserVModel, CreateManagerUserModel, ModifyManagerUserModel>
    {
        Task<string> ModifyManagerUserPassword(int managerUserId, ModifyManagerUserPassWordModel req);
    }
}
