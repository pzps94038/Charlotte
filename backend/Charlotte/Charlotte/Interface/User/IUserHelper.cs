using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerUser;
using Charlotte.Model.User;
using Charlotte.VModel.ManagerUser;
using Charlotte.VModel.Shared;
using Charlotte.VModel.User;

namespace Charlotte.Interface.User
{
    public interface IUserHelper: IGetAllAsync<TableVModel<UsersVModel>>, IDeleteAsync, IBatchDeleteAsync, IModifyAsync<ModifyUserModel>, ICreateAsync<CreateUserModel>
    {
    }
}
