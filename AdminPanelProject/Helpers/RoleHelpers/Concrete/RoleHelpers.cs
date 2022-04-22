using AdminPanel.Models;
using AdminPanelProject.Helpers.Abstract;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Identity;

namespace AdminPanelProject.Helpers;

public class RoleHelpers : IRoleHelpers
{
    private readonly RoleManager<UserRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    public RoleHelpers(RoleManager<UserRole> roleManager, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<DataResult<bool>> AddToUserRole(string userName, string roleName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            return new DataResult<bool>(true, true, null);
        }
        return new DataResult<bool>(false, false, new Exception(result.Errors.ToString()));
    }

    public async Task<DataResult<bool>> CreateRole(string roleName)
    {
        var result = await _roleManager.CreateAsync(new UserRole()
        {
            Name = roleName,
            NormalizedName = roleName
        });

        if (result.Succeeded)
        {
            return new DataResult<bool>(true, true, null);
        }
        return new DataResult<bool>(false, false, new Exception(result.Errors.ToString()));
    }
}

