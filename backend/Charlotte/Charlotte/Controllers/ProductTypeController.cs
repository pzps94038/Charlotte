using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Charlotte.Enum;
using Charlotte.Helper.Product;
using Charlotte.Interface.Product;
using Charlotte.Interface.ProductType;
using Charlotte.Model;
using Charlotte.Services;
using Charlotte.VModel.Product;
using Charlotte.VModel.ProductType;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeHelper _productTypeHelper;
        public ProductTypeController(IProductTypeHelper helper)
        {
            _productTypeHelper = helper;
        }

        /// <summary>
        /// 取得所有產品類型
        /// </summary>
        /// <returns>所有產品類型</returns>
        [HttpGet]
        public async Task<ResultModel<List<ProductTypeVModel>>> GetProductTypes()
        {
            ResultModel<List<ProductTypeVModel>> result = new ResultModel<List<ProductTypeVModel>>();
            try
            {
                result.Data = await _productTypeHelper.GetAllAsync();
                result.Message = EnumUtils.GetDescription(EnumResult.Success);
                result.Code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }
    }
}

