using Charlotte.EnumMessage;
using Charlotte.Helper.ManagerUser;
using Charlotte.Model;
using Charlotte.Model.ManagerUser;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagerUserController : ControllerBase
    {
        private readonly IManagerUserHelper _managerUserHelper;
        public ManagerUserController(IManagerUserHelper helper)
        {
            _managerUserHelper = helper;
        }

        [HttpGet]
        public async Task<ResultModel<List<ManagerUsersVModel>>> GetManagerUsers()
        {
            var result = new ResultModel<List<ManagerUsersVModel>>();
            try
            {
                result.data = await _managerUserHelper.GetManagerUsers();
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
                result.data = await _managerUserHelper.GetManagerUser(managerUserId);
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
                await _managerUserHelper.CreateManagerUser(req);
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
                await _managerUserHelper.ModifyManagerUser(managerUserId, req);
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
        /// 使用者修改密碼
        /// </summary>
        /// <param name="managerUserId">使用者Id</param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("{managerUserId}")]
        public async Task<ResultModel> ModifyManagerUserPassword(int managerUserId, ModifyManagerUserPassWordModel req)
        {
            var result = new ResultModel();
            try
            {
                string errMsg = await _managerUserHelper.ModifyManagerUserPassword(managerUserId, req);
                if (string.IsNullOrEmpty(errMsg))
                {
                    result.code = HttpStatusCode.OK;
                    result.message = "修改密碼成功";
                }
                else 
                {
                    result.code = HttpStatusCode.BadRequest;
                    result.message = errMsg;
                }
            }
            catch (Exception ex)
            {
                result.code = HttpStatusCode.BadRequest;
                result.message = EnumHelper.GetDescription(EnumResult.ModifyFail);
                LoggerHelper.Error(ex);
            }
            return result;
        }

        [HttpDelete("{managerUserId}")]
        public async Task<ResultModel> DeleteManagerUser(int managerUserId)
        {
            var result = new ResultModel();
            try
            {
                await _managerUserHelper.DeleteManagerUser(managerUserId);
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

        [HttpDelete]
        public async Task<ResultModel> BatchDeleteManagerUser(List<int> req)
        {
            var result = new ResultModel();
            try
            {
                await _managerUserHelper.BatchDeleteManagerUser(req);
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
