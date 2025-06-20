using App.Application.Interfaces;

namespace App.Persistance.Implamantations
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
       => context.SaveChangesAsync();
    }
}
