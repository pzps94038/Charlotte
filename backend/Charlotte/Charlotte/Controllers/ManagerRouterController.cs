using Charlotte.Database.Model;
using Charlotte.EnumMessage;
using Charlotte.Helper.ManagerRouter;
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
                result.data = await ManagerRouterHelper.GetRouters();
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
        /// 創建路由
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateRouter(ManagerRouterModel rq)
        {
            var result = new ResultModel();
            try
            {
                await ManagerRouterHelper.CreateRouter(rq);
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
                await ManagerRouterHelper.ModifyRouter(routerId, rq);
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
                await ManagerRouterHelper.DeleteRouter(routerId);
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
                await ManagerRouterHelper.BatchDeleteRouter(rq);
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
