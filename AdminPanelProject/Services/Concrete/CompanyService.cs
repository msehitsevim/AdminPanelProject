using AdminPanel.Models;
using AdminPanelProject.DataAccessLayer.Abstract;
using AdminPanelProject.DataAccessLayer.Concrete;
using AdminPanelProject.Services.Abstract;
using Core.DataResults.Concrete;

namespace AdminPanelProject.Services.Concrete;

public class CompanyService : CompanyRepository, ICompanyService
{
    private readonly ICompanyDal _companyDal;
    public CompanyService(ICompanyDal companyDal)
    {
        _companyDal = companyDal;
    }


    public async Task<DataResult<Company>> GetCompanyByName(string name)
    {

        return (await _companyDal.Get(w => w.CompanyName == name));

    }

}

