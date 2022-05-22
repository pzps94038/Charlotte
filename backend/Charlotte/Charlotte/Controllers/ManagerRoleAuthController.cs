using Charlotte.Enum;
using Charlotte.Helper.ManagerRoleAuth;
using Charlotte.Interface.ManagerRoleAuth;
using Charlotte.Model;
using Charlotte.Model.ManagerRoleAuth;
using Charlotte.Services;
using Charlotte.VModel.ManagerRoleRouter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagerRoleAuthController : ControllerBase
    {
        private readonly IManagerRoleAuthHelper _managerAuthRoleHelper;
        public ManagerRoleAuthController(IManagerRoleAuthHelper helper)
        {
            _managerAuthRoleHelper = helper;
        }
        [HttpGet("{roleId}")]
        public async Task<ResultModel<List<ManagerRoleAuthVModel>>> GetRoleRouters(int roleId) 
        {
            var result = new ResultModel<List<ManagerRoleAuthVModel>>();
            try
            {
                result.data = await _managerAuthRoleHelper.GetManagerRoleRouters(roleId);
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

        [HttpPut("{roleId}")]
        public async Task<ResultModel> ModifyRoleAuth(int roleId, List<ManagerRoleAuthModel> req)
        {
            var result = new ResultModel();
            try
            {
                await _managerAuthRoleHelper.CreateOrUpdateRoleAuth(roleId, req);
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


        [HttpGet]
        public async Task<ResultModel<CheckManagerRoleAuthVModel<bool>>> CheckRoleAuth(int userId, string routerPath) 
        {
            var result = new ResultModel<CheckManagerRoleAuthVModel<bool>>();
            try
            {
                result.data = await _managerAuthRoleHelper.CheckRoleAuth(userId, routerPath);
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

    }
}
