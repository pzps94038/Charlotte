using Charlotte.EnumMessage;
using Charlotte.Helper.ManagerRole;
using Charlotte.Model;
using Charlotte.Model.ManagerRole;
using Charlotte.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerRoleController : ControllerBase
    {
        /// <summary>
        /// 創建權限角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateManagerRole(ManagerRoleModel req)
        {
            var result = new ResultModel();
            try
            {
                await ManagerRoleHelper.CreateManagerRole(req);
                result.code = HttpStatusCode.OK;
                result.message = EnumHelper.GetDescription(EnumResult.CreateSuccess);
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
