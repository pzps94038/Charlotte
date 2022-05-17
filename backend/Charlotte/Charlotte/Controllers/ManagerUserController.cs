using Charlotte.Enum;
using Charlotte.Interface.ManagerUser;
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
                result.data = await _managerUserHelper.GetAllAsync();
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
                result.data = await _managerUserHelper.GetAsync(managerUserId);
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
                await _managerUserHelper.CreateAsync(req);
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
                await _managerUserHelper.ModifyAsync(managerUserId, req);
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
                result.message = EnumUtils.GetDescription(EnumResult.ModifyFail);
                LoggerUtils.Error(ex);
            }
            return result;
        }

        [HttpDelete("{managerUserId}")]
        public async Task<ResultModel> DeleteManagerUser(int managerUserId)
        {
            var result = new ResultModel();
            try
            {
                await _managerUserHelper.DeleteAsync(managerUserId);
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

        [HttpDelete]
        public async Task<ResultModel> BatchDeleteManagerUser(List<int> req)
        {
            var result = new ResultModel();
            try
            {
                await _managerUserHelper.BatchDeleteAsync(req);
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
