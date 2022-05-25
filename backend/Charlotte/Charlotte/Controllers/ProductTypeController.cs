using Charlotte.Enum;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.ProductType;
using Charlotte.Services;
using Charlotte.VModel.Product;
using Charlotte.VModel.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ICRUDAsyncHelper<TableVModel<ProductTypeVModel>, ProductTypeVModel, ProductTypeModel, ProductTypeModel> _productTypeHelper;
        public ProductTypeController(ICRUDAsyncHelper<TableVModel<ProductTypeVModel>, ProductTypeVModel, ProductTypeModel, ProductTypeModel> helper)
        {
            _productTypeHelper = helper;
        }

        /// <summary>
        /// 取得所有產品類型
        /// </summary>
        /// <param name="typeId">產品類型ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<TableVModel<ProductTypeVModel>>> GetProductTypes(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            ResultModel<TableVModel<ProductTypeVModel>> result = new ResultModel<TableVModel<ProductTypeVModel>>();
            try
            {
                result.Data = await _productTypeHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
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

        [HttpPost]
        public async Task<ResultModel> CreateProductType(ProductTypeModel request)
        {
            ResultModel result = new ResultModel();
            try
            {
                await _productTypeHelper.CreateAsync(request);
                result.Message = EnumUtils.GetDescription(EnumResult.CreateSuccess);
                result.Code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.CreateFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 修改產品類型
        /// </summary>
        /// <param name="productTypeId">產品類型ID</param>
        /// <param name="request">產品類型資訊</param>
        /// <returns></returns>
        [HttpPatch("{productTypeId}")]
        public async Task<ResultModel> ModifyProductType(int productTypeId, ProductTypeModel request)
        {
            var result = new ResultModel();
            try
            {
                await _productTypeHelper.ModifyAsync(productTypeId, request);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.ModifySuccess);
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.ModifyFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 刪除產品類型
        /// </summary>
        /// <param name="productTypeId">產品類型ID</param>
        /// <returns></returns>
        [HttpDelete("{productTypeId}")]
        public async Task<ResultModel> DeleteProductType(int productTypeId)
        {
            var result = new ResultModel();
            try
            {
                await _productTypeHelper.DeleteAsync(productTypeId);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 批次刪除產品類型
        /// </summary>
        /// <param name="productTypeId">產品類型ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> BatchDeleteProductTypes(List<int> request)
        {
            var result = new ResultModel();
            try
            {
                await _productTypeHelper.BatchDeleteAsync(request);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }
    }
}
