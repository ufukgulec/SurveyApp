﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SurveyApp.Application.Repositories;
using SurveyApp.Domain.Entities.Common;
using SurveyApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SurveyApp.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SurveyAppContext _context;

        public GenericRepository(SurveyAppContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        #region Select
        #region No Async
        public T? GetById(string id, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(data => data.Id == Guid.Parse(id));
        }
        public T? GetSingle(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable().AsNoTracking();

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefault(expression);
        }

        public List<T> GetList(Expression<Func<T, bool>>? expression = null, bool justActive = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable().AsNoTracking();

            if (justActive)
            {
                query = query.Where(x => x.Status);
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            return query.ToList();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, bool justActive = true, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            if (justActive)
            {
                query = query.Where(x => x.Status);
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }

            return query;
        }
        #endregion
        #region Async
        public async Task<T?> GetByIdAsync(string id, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }
        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? expression = null, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                return await query.FirstOrDefaultAsync(expression);
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }

        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? expression = null, bool justActive = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsNoTracking();
            if (justActive)
            {
                query = query.Where(x => x.Status);
            }

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }

            return await query.ToListAsync();
        }

        #endregion
        #endregion
        #region CRUD
        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            await SaveAsync();
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            await SaveAsync();
            return true;
        }

        public async Task<bool> Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            await SaveAsync();
            return entityEntry.State == EntityState.Modified;
        }

        public bool Remove(string id)
        {
            var entity = Table.Find(id);
            if (entity is not null)
            {
                return Remove(entity);
            }
            else
            {
                return false;
            }

        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            Save();
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            Save();
            return true;
        }

        public bool BulkDeleteById(List<string> ids)
        {
            foreach (var id in ids)
            {
                Remove(id);
            }
            return true;
        }

        public bool BulkDeleteById(Expression<Func<T, bool>> expression)
        {
            var data = GetList(expression);
            return RemoveRange(data);

        }
        #endregion
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }

}
