namespace App.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();   
    }
}
