using Charlotte.Enum;
using Charlotte.Helper.ManagerRole;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Model.ManagerRole;
using Charlotte.Services;
using Charlotte.VModel.ManagerRole;
using Charlotte.VModel.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagerRoleController : ControllerBase
    {
        private readonly ICRUDAsyncHelper<TableVModel<ManagerRoleVModel>, ManagerRoleVModel, ManagerRoleModel, ManagerRoleModel> _managerRoleHelper;
        public ManagerRoleController(ICRUDAsyncHelper<TableVModel<ManagerRoleVModel>, ManagerRoleVModel, ManagerRoleModel, ManagerRoleModel> helper)
        {
            _managerRoleHelper = helper;
        }
        /// <summary>
        /// 取得角色清單
        /// </summary>
        /// <returns>權限角色清單</returns>
        [HttpGet]
        public async Task<ResultModel<TableVModel<ManagerRoleVModel>>> GetRoles(int? limit, int? offset, string? orderBy, string? orderDescription, string? filterStr) 
        {
            var result = new ResultModel<TableVModel<ManagerRoleVModel>> ();
            try
            {
                result.data = await _managerRoleHelper.GetAllAsync(limit, offset, orderBy, orderDescription, filterStr);
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
        /// 創建角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CreateManagerRole(ManagerRoleModel req)
        {
            var result = new ResultModel();
            try
            {
                await _managerRoleHelper.CreateAsync(req);
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
        /// 修改角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="req">角色資料</param>
        /// <returns></returns>
        [HttpPatch("{roleId}")]
        public async Task<ResultModel> ModifyManagerRole(int roleId, ManagerRoleModel req)
        {
            var result = new ResultModel();
            try
            {
                await _managerRoleHelper.ModifyAsync(roleId, req);
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
        /// 權限角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>

        [HttpDelete("{roleId}")]
        public async Task<ResultModel> DeleteManagerRole(int roleId)
        {
            var result = new ResultModel();
            try
            {
                await _managerRoleHelper.DeleteAsync(roleId);
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
        /// 批次刪除角色
        /// </summary>
        /// <param name="req">權限角色Id集合</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> BatchDeleteManagerRole(List<int> req)
        {
            var result = new ResultModel();
            try
            {
                await _managerRoleHelper.BatchDeleteAsync(req);
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
