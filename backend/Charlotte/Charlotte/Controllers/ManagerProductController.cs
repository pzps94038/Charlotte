using Charlotte.Enum;
using Charlotte.Interface.ManagerProduct;
using Charlotte.Model;
using Charlotte.Model.ManagerProduct;
using Charlotte.Services;
using Charlotte.VModel.ManagerProduct;
using Charlotte.VModel.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerProductController : ControllerBase
    {
        private readonly IManagerProductHelper _productHelper;
        public ManagerProductController(IManagerProductHelper helper)
        {
            _productHelper = helper;
        }

        [HttpGet]
        public async Task<ResultModel<TableVModel<ManagerProductVModel>>> GetProducts(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr) 
        {
            ResultModel<TableVModel<ManagerProductVModel>> result = new ResultModel<TableVModel<ManagerProductVModel>>();
            try
            {
                result.Data = await _productHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
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
        public async Task<ResultModel> CresteProduct(ManagerPorductModel request)
        {
            var result = new ResultModel();
            try
            {
                await _productHelper.CreateAsync(request);
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

        [HttpPatch("{productId}")]
        public async Task<ResultModel> ModifyProduct(int productId, ManagerPorductModel request)
        {
            var result = new ResultModel();
            try
            {
                await _productHelper.ModifyAsync(productId, request);
                result.Message = EnumUtils.GetDescription(EnumResult.ModifySuccess);
                result.Code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.ModifyFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("FileUpload")]
        public async Task<ResultModel<string>> FileUpload(List<IFormFile> files)
        {
            var result = new ResultModel<string>();
            try
            {
                result.Data = await _productHelper.FileUpload(files);
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

        [HttpDelete]
        public async Task<ResultModel> BatchDeleteProducts(List<int> request)
        {
            ResultModel result = new ResultModel();
            try
            {
                await _productHelper.BatchDeleteAsync(request);
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
                result.Code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpDelete("{productId}")]
        public async Task<ResultModel> DeleteProduct(int productId)
        {
            ResultModel result = new ResultModel();
            try
            {
                await _productHelper.DeleteAsync(productId);
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
                result.Code = HttpStatusCode.OK;
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
