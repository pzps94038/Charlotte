using Charlotte.EnumMessage;
using Charlotte.Helper.ManagerRoleAuth;
using Charlotte.Model;
using Charlotte.Model.ManagerRoleAuth;
using Charlotte.Services;
using Charlotte.VModel.ManagerRoleRouter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerRoleAuthController : ControllerBase
    {
        [HttpGet("{roleId}")]
        public async Task<ResultModel<List<ManagerRoleAuthVModel>>> GetRoleRouters(int roleId) 
        {
            var result = new ResultModel<List<ManagerRoleAuthVModel>>();
            try
            {
                result.data = await ManagerRoleAuthHeper.GetManagerRoleRouters(roleId);
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

        [HttpPut("{roleId}")]
        public async Task<ResultModel> ModifyRoleAuth(int roleId, List<ManagerRoleAuthModel> req)
        {
            var result = new ResultModel();
            try
            {
                await ManagerRoleAuthHeper.CreateOrUpdateRoleAuth(roleId, req);
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


        [HttpGet]
        public async Task<ResultModel<CheckManagerRoleAuthVModel<bool>>> CheckRoleAuth(int userId, string routerPath) 
        {
            var result = new ResultModel<CheckManagerRoleAuthVModel<bool>>();
            try
            {
                result.data = await ManagerRoleAuthHeper.CheckRoleAuth(userId, routerPath);
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

    }
}
