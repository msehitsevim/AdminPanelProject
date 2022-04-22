using AdminPanelProject.Enums;
using AdminPanelProject.Helpers.Abstract;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{

    private readonly IRoleHelpers _roleHelpers;
    public RoleController(IRoleHelpers roleHelpers)
    {
        _roleHelpers = roleHelpers;

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SuperAdmin)]
    [Route("CreateRole")]
    [HttpGet]
    public async Task<DataResult<bool>> CreateRole(string roleName)
    {
        return await _roleHelpers.CreateRole(roleName);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
    [Route("AddToUserRole")]
    [HttpGet]
    public async Task<DataResult<bool>> AddToUserRoleByName(string userName, string roleName)
    {
        if (roleName == "SuperAdmin")
        {
             return new DataResult<bool>(false, false, new Exception("you are not authorized"));
        }
        return await _roleHelpers.AddToUserRole(userName, roleName);
    }
}

