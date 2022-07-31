using Charlotte.Enum;
using Charlotte.Interface.ManagerUser;
using Charlotte.Interface.User;
using Charlotte.Model;
using Charlotte.Model.ManagerUser;
using Charlotte.Model.User;
using Charlotte.Services;
using Charlotte.VModel.ManagerUser;
using Charlotte.VModel.Shared;
using Charlotte.VModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IHubContext<DashbordHub> _hubContext;
        public UserController(IUserHelper helper, IHubContext<DashbordHub> hubContext)
        {
            _userHelper = helper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ResultModel<TableVModel<UsersVModel>>> GetUsers(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr)
        {
            var result = new ResultModel<TableVModel<UsersVModel>>();
            try
            {
                result.Data = await _userHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
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
        /// 創建使用者
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateUser(CreateUserModel req)
        {
            var result = new ResultModel();
            try
            {
                await _userHelper.CreateAsync(req);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.CreateSuccess);
                await _hubContext.Clients.All.SendAsync("RegisteredMemberRefresh");
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
        /// <param name="userId">使用者Id</param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPatch("{userId}")]
        public async Task<ResultModel> ModifyUser(int userId, ModifyUserModel req)
        {
            var result = new ResultModel();
            try
            {
                await _userHelper.ModifyAsync(userId, req);
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


        [HttpDelete("{userId}")]
        public async Task<ResultModel> DeleterUser(int userId)
        {
            var result = new ResultModel();
            try
            {
                await _userHelper.DeleteAsync(userId);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
                await _hubContext.Clients.All.SendAsync("RegisteredMemberRefresh");
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
        public async Task<ResultModel> BatchDeleteUser(List<int> req)
        {
            var result = new ResultModel();
            try
            {
                await _userHelper.BatchDeleteAsync(req);
                result.Code = HttpStatusCode.OK;
                result.Message = EnumUtils.GetDescription(EnumResult.DeleteSuccess);
                await _hubContext.Clients.All.SendAsync("RegisteredMemberRefresh");
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
