using AdminPanel.Models;
using AdminPanelProject.Business.Abstract;
using AdminPanelProject.Services.Abstract;
using AdminPanelProject.ViewModels;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace AdminPanelProject.Business.Concrete;

public class CompanyManager : ICompanyManager
{
    private readonly ICompanyService _companyService;
    private readonly UserManager<AppUser> _userManager;
    public CompanyManager(ICompanyService companyService, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _companyService = companyService;
    }

    public async Task<DataResult<Company>> AddAsync(Company entity)
    {
        var results = await _companyService.AddAsync(entity);
        try
        {
            if (results.Success)
            {
                var saveResult = (await _companyService.SaveChangesAsync());
                if (saveResult.Success)
                {
                    return results;
                }
            }
            return results;
        }
        catch (Exception ex)
        {
            results.Error = ex;
            results.Success = false;
            return results;
        }
    }

    public async Task<DataResult<bool>> AddCompanyToUserById(AddCompanyToUserViewModel addCompanyToUserViewModel)
    {
        if(addCompanyToUserViewModel.CompanyId != null && addCompanyToUserViewModel.UserId != null)
        {
            var user = await _userManager.FindByIdAsync(addCompanyToUserViewModel.UserId.ToString());
            if (user != null)
            {
                user.CompanyId = addCompanyToUserViewModel.CompanyId;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new DataResult<bool>(true, true, null);
                }
            }
            return new DataResult<bool>(false, false, new Exception("UserId/CompanyId cannot be null"));
        }
        else
        {
            return new DataResult<bool>(false, false, new Exception("UserId/CompanyId cannot be null"));
        }
       
    }

    public Task<DataResult<List<Company>>> AddRangeAsync(List<Company> entites)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<IQueryable<Company>>> AsNoTrackingAsync(Func<Company, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<IQueryable<Company>>> AsNoTrackingAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<Company>> DeleteAsync(Company entity)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<List<Company>>> DeleteAsync(List<Company> entities)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<Company>> DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<Company>> Get(Func<Company, bool>? predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Company> Get(Expression<Func<Company, bool>> filter = null, Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null, string includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public async Task<DataResult<IQueryable<Company>>> GetAllAsync()
    {
        return await _companyService.GetAllAsync();
    }

    public async Task<DataResult<IQueryable<Company>>> GetAllAsync(Func<Company, bool>? predicate)
    {
        return await _companyService.GetAllAsync(predicate);
    }

    public Task<DataResult<IQueryable<Company>>> GetAllLazyLoad(Expression<Func<Company, bool>> filter, params Expression<Func<Company, object>>[] children)
    {
        throw new NotImplementedException();
    }

    public async Task<DataResult<Company>> GetByIdAsync(Guid id)
    {
        return await _companyService.GetByIdAsync(id);
    }

    public async Task<DataResult<Company>> GetCompanyByName(string name)
    {

        return await _companyService.GetCompanyByName(name);
    }

    public Task<DataResult<int>> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<IQueryable<Company>>> Take(int count)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<List<Company>>> ToListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<List<Company>>> ToListAsync(Func<Company, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<int>> Update(Company entity)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<int>> UpdateRange(List<Company> entities)
    {
        throw new NotImplementedException();
    }

    public Task<DataResult<IQueryable<Company>>> Where(Func<Company, bool> predicate)
    {
        throw new NotImplementedException();
    }
}

