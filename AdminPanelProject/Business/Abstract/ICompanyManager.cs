using AdminPanel.Models;
using AdminPanelProject.Services.Abstract;
using AdminPanelProject.ViewModels;
using Core.DataResults.Concrete;

namespace AdminPanelProject.Business.Abstract;

public interface ICompanyManager : ICompanyService
{
    public Task<DataResult<Company>> GetCompanyByName(string name);
    public Task<DataResult<bool>> AddCompanyToUserById(AddCompanyToUserViewModel addCompanyToUserViewModel);
}

