using Charlotte.EnumMessage;
using Charlotte.Helper.Factory;
using Charlotte.Model;
using Charlotte.Model.Factory;
using Charlotte.Services;
using Charlotte.VModel.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FactoryController : ControllerBase
    {
        private readonly IFactoryHelper _factoryHelper;
        public FactoryController(IFactoryHelper helper) 
        {
            _factoryHelper = helper;
        }

        /// <summary>
        /// 取得多個廠商資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<FactoryVModel>>> GetFactorys()
        {
            var result = new ResultModel<List<FactoryVModel>>();
            try
            {
                result.data = await _factoryHelper.GetFactorys();
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.Success);

            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.Fail);
                LoggerHelper.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 取得單個廠商資料
        /// </summary>
        /// <param name="factoryId">廠商Id</param>
        /// <returns></returns>
        [HttpGet("{factoryId}")]
        public async Task<ResultModel<FactoryVModel>> GetFactory(int factoryId)
        {
            var result = new ResultModel<FactoryVModel>();
            try
            {
                result.data = await _factoryHelper.GetFactory(factoryId);
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.Success);

            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.Fail);
                LoggerHelper.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// 新增廠商
        /// </summary>
        /// <param name="req">廠商資訊</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateFactory(Factory req)
        {
            var result = new ResultModel();
            try
            {
                await _factoryHelper.CreateFactory(req.factoryName);
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.CreateSuccess);

            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.CreateFail);
                LoggerHelper.Error(ex);
            }
            return result;
        }


        /// <summary>
        /// 修改廠商
        /// </summary>
        /// <param name="req">廠商資訊</param>
        /// <returns></returns>
        [HttpPatch("{factoryId}")]
        public async Task<ResultModel> ModifyFactory(int factoryId, Factory req)
        {
            var result = new ResultModel();
            try
            {
                await _factoryHelper.ModifyFactory(factoryId, req.factoryName);
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.ModifySuccess);

            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.ModifyFail);
                LoggerHelper.Error(ex);
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
                await _factoryHelper.DeleteFactory(factoryId);
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.DeleteSuccess);

            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.DeleteFail);
                LoggerHelper.Error(ex);
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
                await _factoryHelper.BatchDeleteFactory(rq);
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.DeleteSuccess);

            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.DeleteFail);
                LoggerHelper.Error(ex);
            }
            return result;
        }
    }
}
