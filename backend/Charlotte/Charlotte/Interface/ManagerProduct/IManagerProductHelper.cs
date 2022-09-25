using Charlotte.Interface.Shared;
using Charlotte.Model.ManagerProduct;
using Charlotte.VModel.ManagerProduct;
using Charlotte.VModel.Shared;

namespace Charlotte.Interface.ManagerProduct
{
    public interface IManagerProductHelper: IGetAllAsync<TableVModel<ManagerProductVModel>>, IModifyAsync<ManagerPorductModel>, ICreateAsync<ManagerPorductModel>, IDeleteAsync, IBatchDeleteAsync
    {
        public Task<string> FileUpload(List<IFormFile> files);
        public Task<string> EditorFileUpload(List<IFormFile> files);
    }
}
    