﻿using System.Linq.Expressions;

namespace App.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
