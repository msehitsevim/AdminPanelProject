using AdminPanel.Models;
using AdminPanelProject.Enums;
using AdminPanelProject.Helpers.UserHelpers.Abstract;
using AdminPanelProject.ViewModels;
using Core.Authentication.Abstract;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Identity;

namespace AdminPanelProject.Helpers.UserHelpers.Concrete;

public class UserHelpers : IUserHelpers
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtAuthentication jwtAuthentication;
    public UserHelpers(UserManager<AppUser> userManager, IJwtAuthentication jwtAuthentication)
    {
        this.jwtAuthentication = jwtAuthentication;
        _userManager = userManager;
    }



    public async Task<DataResult<bool>> CreateAsync(RegisterViewModel registerViewModel)
    {
        var appUser = new AppUser();
        appUser.Email = registerViewModel.Email;
        appUser.UserName = registerViewModel.Username;
        appUser.CompanyId = registerViewModel.CompanyId;
        var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);
        var roleResult = await _userManager.AddToRoleAsync(appUser, Roles.Member);

        if (result.Succeeded && roleResult.Succeeded)
        {
            await jwtAuthentication.Authentication(new LoginViewModel()
            {
                Username = registerViewModel.Username,
                Password = registerViewModel.Password
            });
            return new DataResult<bool>(true, true, null);
        }
        return new DataResult<bool>(false, false, new Exception(result.Errors?.ToString() + roleResult.Errors?.ToString()));
    }
}

