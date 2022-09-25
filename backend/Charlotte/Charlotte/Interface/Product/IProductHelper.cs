using Charlotte.Interface.Shared;
using Charlotte.VModel.Product;

namespace Charlotte.Interface.Product
{
    public interface IProductHelper: IGetAsync<ProductVModel>
    {
        Task<List<ProductVModel>> GetProducts(int? typeId);
        Task<List<SearchVModel>> Search(string search);
    }
}
