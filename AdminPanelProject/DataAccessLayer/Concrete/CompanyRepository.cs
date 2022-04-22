using AdminPanel.Models;
using AdminPanelProject.DataAccessLayer.Abstract;
using Core.Context.EFContext;
using Core.EFRepository.Concrete;

namespace AdminPanelProject.DataAccessLayer.Concrete;

public class CompanyRepository : EFRepository<Company, CompanyContext>, ICompanyDal
{
}

