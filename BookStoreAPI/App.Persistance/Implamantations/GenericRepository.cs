using System.Linq.Expressions;
using App.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Implamantations
{
    public class GenericRepository<T> (AppDbContext context): IGenericRepository<T> where T : class
    {
         private readonly DbSet<T> _dbSet = context.Set<T>();

        public async ValueTask AddAsync(T entity)=> await _dbSet.AddAsync(entity);

        public void DeleteAsync(T entity) => _dbSet.Remove(entity);


        public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();


        public ValueTask<T?> GetByIdAsync(int id)=>_dbSet.FindAsync(id);


        public void UpdateAsync(T entity) => _dbSet.Update(entity);


        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        => _dbSet.Where(predicate).AsNoTracking();
    }
}
