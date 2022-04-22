using AdminPanelProject.ViewModels;
using Core.DataResults.Concrete;

namespace AdminPanelProject.Helpers.UserHelpers.Abstract;

public interface IUserHelpers
{
    public Task<DataResult<bool>> CreateAsync(RegisterViewModel registerViewModel);
}

