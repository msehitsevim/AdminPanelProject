using AdminPanelProject.Helpers.UserHelpers.Abstract;
using AdminPanelProject.ViewModels;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRegisterController : ControllerBase
{

    private readonly IUserHelpers _userHelpers;
    public UserRegisterController(IUserHelpers userHelpers)
    {
        _userHelpers = userHelpers;

    }
    [Route("UserRegister")]
    [HttpPost]
    public async Task<DataResult<bool>> UserRegister(RegisterViewModel registerViewModel)
    {
        return await _userHelpers.CreateAsync(registerViewModel);
    }
}

