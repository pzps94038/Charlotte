using Charlotte.Enum;
using Charlotte.Helper.Factory;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.Factory;
using Charlotte.Services;
using Charlotte.VModel.Factory;
using Charlotte.VModel.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ManagerUser")]
    public class ManagerFactoryController : ControllerBase
    {
        private readonly ICRUDAsyncHelper<TableVModel<ManagerFactoryVModel>, ManagerFactoryVModel, string, string> _factoryHelper;
        public ManagerFactoryController(ICRUDAsyncHelper<TableVModel<ManagerFactoryVModel>, ManagerFactoryVModel, string, string> helper) 
        {
            _factoryHelper = helper;
        }

        /// <summary>
        /// 取得多個廠商資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<TableVModel<ManagerFactoryVModel>>> GetFactorys(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            var result = new ResultModel<TableVModel<ManagerFactoryVModel>>();
            try
            {
                result.Data = await _factoryHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.Success);

            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 取得單個廠商資料
        /// </summary>
        /// <param name="factoryId">廠商Id</param>
        /// <returns></returns>
        [HttpGet("{factoryId}")]
        public async Task<ResultModel<ManagerFactoryVModel>> GetFactory(int factoryId)
        {
            var result = new ResultModel<ManagerFactoryVModel>();
            try
            {
                result.Data = await _factoryHelper.GetAsync(factoryId);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.Success);

            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.Fail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 新增廠商
        /// </summary>
        /// <param name="req">廠商資訊</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateFactory(ManagerFactory req)
        {
            var result = new ResultModel();
            try
            {
                await _factoryHelper.CreateAsync(req.FactoryName);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.CreateSuccess);

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
        /// 修改廠商
        /// </summary>
        /// <param name="req">廠商資訊</param>
        /// <returns></returns>
        [HttpPatch("{factoryId}")]
        public async Task<ResultModel> ModifyFactory(int factoryId, ManagerFactory req)
        {
            var result = new ResultModel();
            try
            {
                await _factoryHelper.ModifyAsync(factoryId, req.FactoryName);
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
        /// 刪除廠商
        /// </summary>
        /// <param name="factoryId">廠商Id</param>
        /// <returns></returns>
        [HttpDelete("{factoryId}")]
        public async Task<ResultModel> DeleteFactory(int factoryId)
        {
            var result = new ResultModel();
            try
            {
                await _factoryHelper.DeleteAsync(factoryId);
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
        /// 批次刪除廠商
        /// </summary>
        /// <param name="rq">多個廠商Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> BatchDeleteFactory(List<int> rq)
        {
            var result = new ResultModel();
            try
            {
                await _factoryHelper.BatchDeleteAsync(rq);
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
