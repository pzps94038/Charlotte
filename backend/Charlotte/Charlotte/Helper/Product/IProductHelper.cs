using Charlotte.VModel.Product;

namespace Charlotte.Helper.Product
{
    public interface IProductHelper
    {
        Task<ProductVModel> GetProruct(int productId);
        Task<List<ProductVModel>> GetProducts(int? typeId);
    }
}
