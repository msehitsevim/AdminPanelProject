using AdminPanel.Models;
using AdminPanelProject.Business.Abstract;
using AdminPanelProject.Enums;
using AdminPanelProject.ViewModels;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{

    private readonly ICompanyManager _companyManager;
    public CompanyController(ICompanyManager companyManager)
    {

        _companyManager = companyManager;
    }
    [Route("AddCompany")]
    [HttpPost]
    public async Task<DataResult<Company>> AddCompany(Company company)
    {
        return await _companyManager.AddAsync(company);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SuperAdmin)]
    [Route("GetAllCompanies")]
    [HttpGet]
    public async Task<DataResult<IQueryable<Company>>> GetAllCompanies()
    {
        return await _companyManager.GetAllAsync();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SuperAdmin)]
    [Route("AddCompanyToUserById")]
    [HttpPost]
    public async Task<DataResult<bool>> AddCompanyToUserById(AddCompanyToUserViewModel? addCompanyToUserViewModel)
    {
        return await _companyManager.AddCompanyToUserById(addCompanyToUserViewModel);
    }
}

