using Charlotte.Enum;
using Charlotte.Services;
using Charlotte.VModel.Product;
using Dapper;
using System.Data.SqlClient;

namespace Charlotte.Helper.Product
{
    public class ProductHelper : IProductHelper
    {
        /// <summary>
        /// 取得產品
        /// </summary>
        /// <param name="productId">產品ID</param>
        /// <returns>符合該產品ID的產品</returns>
        public async Task<ProductVModel> GetProruct(int productId) 
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr)) 
            {
                await con.OpenAsync();
                string sqlStr = @"select product.ProductId, product.ProductName, type.Type, product.Inventory, product.SellPrice
                                from ProductDetails as product 
                                left join ProductType as type on product.ProductTypeId = type.ProductTypeId
                                Where ProductId = @productId";
                return await con.QueryFirstOrDefaultAsync<ProductVModel>(sqlStr, new { productId = productId });
            }
        }

        /// <summary>
        /// 取得該產品類型的所有產品
        /// </summary>
        /// <param name="typeId">產品類型ID</param>
        /// <returns>該產品類型的所有產品</returns>
        public async Task<List<ProductVModel>> GetProducts(int? typeId) 
        {
            string sqlConStr = GetAppSettingsUtils.GetConnectionString(EnumUtils.GetDescription(EnumDataBase.Charlotte));
            using (SqlConnection con = new SqlConnection(sqlConStr)) 
            {
                await con.OpenAsync();
                string sqlStr = @"select product.ProductId, product.ProductName, type.Type, product.Inventory, product.SellPrice
                            from ProductDetails as product 
                            left join ProductType as type on product.ProductTypeId = type.ProductTypeId";
                DynamicParameters parameters = new DynamicParameters();
                if (typeId != null)
                {
                    sqlStr += "Where type.ProductTypeId = @typeId";
                    parameters.Add("typeId", typeId);
                }
                var result = await con.QueryAsync<ProductVModel>(sqlStr, parameters);
                return result.ToList();
            }
        }
    }
}
