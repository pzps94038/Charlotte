using Charlotte.Database.Model;
using Charlotte.Enum;
using Charlotte.Helper.ManagerRouter;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.ManagerRouter;
using Charlotte.Services;
using Charlotte.VModel.ManagerRouter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagerRouterController : ControllerBase
    {
        private readonly ICRUDAsyncHelper<ManagerRouterVModel, ManagerRouterVModel, ManagerRouterModel, ManagerRouterModel> _managerRouterHelper;
        public ManagerRouterController(ICRUDAsyncHelper<ManagerRouterVModel, ManagerRouterVModel, ManagerRouterModel, ManagerRouterModel> helper)
        {
            _managerRouterHelper = helper;
        }

        /// <summary>
        /// 取得所有路由清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<ManagerRouterVModel>>> GetRouters()
        {
            var result = new ResultModel<List<ManagerRouterVModel>>();
            try
            {
                result.data = await _managerRouterHelper.GetAllAsync();
                result.code = HttpStatusCode.OK;
                result.message = EnumUtils.GetDescription(EnumResult.Success);
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
                result.code = HttpStatusCode.OK;
                result.message = EnumUtils.GetDescription(EnumResult.CreateSuccess);
            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumUtils.GetDescription(EnumResult.CreateFail);
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
                result.code = HttpStatusCode.OK;
                result.message = EnumUtils.GetDescription(EnumResult.ModifySuccess);
            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumUtils.GetDescription(EnumResult.ModifyFail);
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
                result.code = HttpStatusCode.OK;
                result.message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumUtils.GetDescription(EnumResult.DeleteFail);
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
                result.code = HttpStatusCode.OK;
                result.message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumUtils.GetDescription(EnumResult.DeleteFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }
    }
}
