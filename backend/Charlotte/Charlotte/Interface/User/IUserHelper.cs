using Charlotte.Interface.Shared;
using Charlotte.Model.User;
using Charlotte.VModel.Shared;
using Charlotte.VModel.User;

namespace Charlotte.Interface.User
{
    public interface IUserHelper : IGetAllAsync<TableVModel<UserVModel>>, IDeleteAsync, IBatchDeleteAsync, IModifyAsync<UserModel>, ICreateAsync<UserModel>
    {
    }
}
