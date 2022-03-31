using Charlotte.EnumMessage;
using Charlotte.Helper.ManagerUser;
using Charlotte.Model;
using Charlotte.Model.ManagerUser;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerUserController : ControllerBase
    {
        /// <summary>
        /// 創建管理平台使用者
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateManagerUser(CreateManagerUserModel req)
        {
            var result = new ResultModel();
            try
            {
                await ManagerUserHelper.CreateManagerUser(req);
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
        /// 取該使用者資訊
        /// </summary>
        /// <param name="managerUserId">使用者Id</param>
        /// <returns></returns>
        [HttpGet("{managerUserId}")]
        public async Task<ResultModel<ManagerUserVModel>> GetManagerUser(int managerUserId)
        {
            var result = new ResultModel<ManagerUserVModel>();
            try
            {
                result.data = await ManagerUserHelper.GetManagerUser(managerUserId);
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
        /// 修改該使用者資料
        /// </summary>
        /// <param name="managerUserId">使用者Id</param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPatch("{managerUserId}")]
        public async Task<ResultModel> ModifyManagerUser(int managerUserId, ModifyManagerUserModel req)
        {
            var result = new ResultModel();
            try
            {
                await ManagerUserHelper.ModifyManagerUser(managerUserId, req);
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
    }
}
