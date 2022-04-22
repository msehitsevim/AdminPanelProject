using AdminPanel.Models;
using AdminPanelProject.DataAccessLayer.Abstract;
using Core.DataResults.Concrete;

namespace AdminPanelProject.Services.Abstract;

public interface ICompanyService : ICompanyDal
{
    public Task<DataResult<Company>> GetCompanyByName(string name);
    

}

