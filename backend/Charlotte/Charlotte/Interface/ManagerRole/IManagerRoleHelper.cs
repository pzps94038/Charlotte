using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerRole;
using Charlotte.Model.ManagerRoleAuth;
using Charlotte.VModel.ManagerRole;
using Charlotte.VModel.ManagerRoleRouter;
using Charlotte.VModel.Shared;

namespace Charlotte.Interface.ManagerRole
{
    public interface IManagerRoleHelper: IGetAllAsync<TableVModel<ManagerRoleVModel>>, IDeleteAsync, IBatchDeleteAsync, IModifyAsync<ManagerRoleModel>, ICreateAsync<ManagerRoleModel>
    {

    }
}
