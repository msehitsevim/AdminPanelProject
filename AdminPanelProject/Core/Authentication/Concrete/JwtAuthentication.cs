using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdminPanel.Models;
using AdminPanelProject.ViewModels;
using Core.Authentication.Abstract;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Core.Authentication.Concrete;

public class JwtAuthentication : IJwtAuthentication
{

    private readonly UserManager<AppUser> _userManager;

    public JwtAuthentication(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<DataResult<AppUser>> Authentication(LoginViewModel loginViewModel)
    {
        var loginUser = await _userManager.FindByNameAsync(loginViewModel.Username);

        if (loginUser == null || loginUser.UserName != loginViewModel.Username && loginUser.PasswordHash != loginViewModel.Password)
        {
            return new DataResult<AppUser>(null, false, new Exception("Username or password wrong"));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("This is my Secret Key.");
        var roles = await _userManager.GetRolesAsync(loginUser);

        var roleClaims = new ClaimsIdentity();
        foreach (var role in roles)
        {
            roleClaims.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        roleClaims.AddClaim(new Claim(ClaimTypes.Name, loginUser.Id.ToString()));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = roleClaims,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        loginUser.Token = tokenHandler.WriteToken(token);
        await _userManager.UpdateAsync(loginUser);

        loginUser.PasswordHash = null;

        return new DataResult<AppUser>(loginUser, true, null);

    }
}
