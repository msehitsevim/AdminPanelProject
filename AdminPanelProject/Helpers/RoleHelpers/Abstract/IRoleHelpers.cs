using Core.DataResults.Concrete;

namespace AdminPanelProject.Helpers.Abstract;

public interface IRoleHelpers
{
    public Task<DataResult<bool>> AddToUserRole(string userName, string roleName);
    public Task<DataResult<bool>> CreateRole(string roleName);

}

