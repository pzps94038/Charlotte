using Charlotte.Database.Model;
using Charlotte.Enum;
using Charlotte.Helper.ManagerRouter;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.ManagerRouter;
using Charlotte.Services;
using Charlotte.VModel.ManagerRouter;
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
    public class ManagerRouterController : ControllerBase
    {
        private readonly ICRUDAsyncHelper<TableVModel<ManagerRouterVModel>, ManagerRouterVModel, ManagerRouterModel, ManagerRouterModel> _managerRouterHelper;
        public ManagerRouterController(ICRUDAsyncHelper<TableVModel<ManagerRouterVModel>, ManagerRouterVModel, ManagerRouterModel, ManagerRouterModel> helper)
        {
            _managerRouterHelper = helper;
        }

        /// <summary>
        /// 取得所有路由清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<TableVModel<ManagerRouterVModel>>> GetRouters(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            var result = new ResultModel<TableVModel<ManagerRouterVModel>>();
            try
            {
                result.Data = await _managerRouterHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
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
        /// 創建路由
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateRouter(ManagerRouterModel rq)
        {
            var result = new ResultModel();
            try
            {
                await _managerRouterHelper.CreateAsync(rq);
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
        /// 修改路由
        /// </summary>
        /// <param name="routerId">要修改的路由ID</param>
        /// <param name="rq">修改參數</param>
        /// <returns></returns>
        [HttpPatch("{routerId}")]
        public async Task<ResultModel> ModifyRouter(int routerId, ManagerRouterModel rq) 
        {
            var result = new ResultModel();
            try
            {
                await _managerRouterHelper.ModifyAsync(routerId, rq);
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
        /// 刪除路由
        /// </summary>
        /// <param name="routerId">要刪除的路由ID</param>
        /// <returns></returns>
        [HttpDelete("{routerId}")]
        public async Task<ResultModel> DeleteRouter(int routerId)
        {
            var result = new ResultModel();
            try
            {
                await _managerRouterHelper.DeleteAsync(routerId);
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
        /// 批次刪除路由
        /// </summary>
        /// <param name="rq">要刪除的路由ID集合</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> BatchDeleteRouter(List<int> rq) 
        {
            var result = new ResultModel();
            try
            {
                await _managerRouterHelper.BatchDeleteAsync(rq);
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
