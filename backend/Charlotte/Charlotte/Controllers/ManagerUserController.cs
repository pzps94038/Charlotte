using Charlotte.Enum;
using Charlotte.Interface.ManagerUser;
using Charlotte.Model;
using Charlotte.Model.ManagerUser;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Charlotte.VModel.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ManagerUserController : ControllerBase
    {
        private readonly IManagerUserHelper _managerUserHelper;
        public ManagerUserController(IManagerUserHelper helper)
        {
            _managerUserHelper = helper;
        }

        [HttpGet]
        public async Task<ResultModel<TableVModel<ManagerUsersVModel>>> GetManagerUsers(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            var result = new ResultModel<TableVModel<ManagerUsersVModel>>();
            try
            {
                result.Data = await _managerUserHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
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
                result.Data = await _managerUserHelper.GetAsync(managerUserId);
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
                    result.Code = HttpStatusCode.OK;
                    result.Message = "修改密碼成功";
                }
                else 
                {
                    result.Code = HttpStatusCode.BadRequest;
                    result.Message = errMsg;
                }
            }
            catch (Exception ex)
            {
                result.Code = HttpStatusCode.BadRequest;
                result.Message = EnumUtils.GetDescription(EnumResult.ModifyFail);
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

        [HttpDelete]
        public async Task<ResultModel> BatchDeleteManagerUser(List<int> req)
        {
            var result = new ResultModel();
            try
            {
                await _managerUserHelper.BatchDeleteAsync(req);
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
