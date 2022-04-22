using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.DataResults.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrudBase.Abstract
{
    public interface ICrudBase<T> where T:class
    {
        Task<HttpStatusCode> UpdateFromList(List<T> entities);
        Task<HttpStatusCode> Update(T entity);
        Task<HttpStatusCode> Create(T entity);
        Task<HttpStatusCode> CreateFromList(List<T> entities);
        Task<HttpStatusCode> DeleteById(Guid id);
        Task<HttpStatusCode> DeleteFromIdList(List<Guid> idList);
        Task<HttpStatusCode> DeleteFromList(List<T> entities);
        Task<DataResult<IQueryable<T>>> GetAll();
    }
}