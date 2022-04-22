using AdminPanel.Models;
using AdminPanelProject.ViewModels;
using Core.DataResults.Concrete;

namespace Core.Authentication.Abstract;

public interface IJwtAuthentication
{
    public Task<DataResult<AppUser>> Authentication(LoginViewModel loginViewModel);
}
