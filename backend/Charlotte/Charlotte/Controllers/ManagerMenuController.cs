﻿using Charlotte.Enum;
using Charlotte.Helper.ManagerMenu;
using Charlotte.Interface.Shared;
using Charlotte.Model;
using Charlotte.Services;
using Charlotte.VModel.ManagerMenu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Charlotte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ManagerUser")]
    public class ManagerMenuController : ControllerBase
    {

        private readonly IGetAsync<List<ManagerMenuVModel>> _menuHelper;
        public ManagerMenuController(IGetAsync<List<ManagerMenuVModel>> helper)
        {
            _menuHelper = helper;
        }

        [HttpGet("{userId}")]
        public async Task<ResultModel<List<ManagerMenuVModel>>> GetMenu(int userId)
        {
            ResultModel<List<ManagerMenuVModel>> result = new ResultModel<List<ManagerMenuVModel>>();
            try
            {
                result.Data = await _menuHelper.GetAsync(userId);
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
    }
}
