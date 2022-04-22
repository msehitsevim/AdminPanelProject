using AdminPanel.Models;
using AdminPanelProject.Business.Abstract;
using AdminPanelProject.ViewModels;
using Core.Authentication.Abstract;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AdminPanelProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{

    private readonly IJwtAuthentication _jwtAuthentication;
    private readonly ICompanyManager _companyManager;
    private readonly UserManager<AppUser> _userManager;

    public AuthenticationController(IJwtAuthentication jwtAuthentication,ICompanyManager companyManager,UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _companyManager = companyManager;
        _jwtAuthentication = jwtAuthentication;
    }

    [Route("CreateToken")]
    [HttpPost]
    public async Task<DataResult<AppUser>> CreateToken(LoginViewModel loginViewModel)
    {
        return await _jwtAuthentication.Authentication(loginViewModel);
    }

   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = Roles.SuperAdmin )]
    [Route("GetUserCompany")]
    [HttpGet]
    public Company GetUserCompany()
    {
     
        var req = Request.Headers.Authorization;
        var auth = req.Where(w => w.Contains("Bearer")).FirstOrDefault().Split(" ");
        string token = "";
        var handler = new JwtSecurityTokenHandler();
        if(auth.Length == 2 && auth[0] == "Bearer")
        {
            token = auth[1];
        }

        var decodedToken = handler.ReadJwtToken(token);
        var claims = decodedToken.Claims.ToList();
   
        var userId = claims.FirstOrDefault(w => w.Type == "name")?.Value;
        if(Guid.TryParse(userId,out Guid userid))
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            var company = user.CompanyId != null ? _companyManager.GetByIdAsync(user.CompanyId.Value).Result.Result : null;
            return company;
        }

        return null;
    }

    //[CustomAuthorize(Role:"Admin")]
    //[Route("CustomAuth")]
    //[HttpGet]
    //public string CustomAuthrorize()
    //{
    //    return string.Empty;
    //}

}

