using Microsoft.EntityFrameworkCore;
using SurveyApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);

        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Remove(string id);
        bool Remove(T entity);
        bool RemoveRange(List<T> entities);
        Task<bool> Update(T entity);
        Task<int> SaveAsync();
    }
}
