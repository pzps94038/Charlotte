using Charlotte.Enum;
using Charlotte.Helper.Product;
using Charlotte.Model;
using Charlotte.Services;
using Charlotte.VModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductHelper _productHelper;
        public ProductController(IProductHelper helper)
        {
            _productHelper = helper;
        }
        /// <summary>
        /// 取得單個產品資料
        /// </summary>
        /// <param name="productId">產品ID</param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        
        public async Task<ResultModel<ProductVModel>> GetProduct(int productId) 
        {
            ResultModel<ProductVModel> result = new ResultModel<ProductVModel>();            
            try
            {
                var data = await _productHelper.GetProruct(productId);
                if (data == null)
                {
                    result.message = EnumUtils.GetDescription(EnumResult.NotFound);
                    result.code = HttpStatusCode.NotFound;
                }
                else 
                {
                    result.message = EnumUtils.GetDescription(EnumResult.Success);
                    result.code = HttpStatusCode.OK;
                    result.data = data;
                }
                
            }
            catch (Exception ex) 
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 產品類型ID
        /// </summary>
        /// <param name="typeId">產品類型ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<ProductVModel>>> GetProducts(int? typeId)
        {
            ResultModel<List<ProductVModel>> result = new ResultModel<List<ProductVModel>>();
            try
            {
                result.data = await _productHelper.GetProducts(typeId);
                result.message = EnumUtils.GetDescription(EnumResult.Success);
                result.code= HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }
        
    }
}
