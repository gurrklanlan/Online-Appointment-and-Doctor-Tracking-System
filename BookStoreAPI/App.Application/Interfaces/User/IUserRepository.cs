namespace App.Application.Interfaces.User
{
    public interface IUserRepository
    {

        Task<App.Domain.Entites.User?> GetByEmailAsync(string email);
        Task<App.Domain.Entites.User?> GetByRefreshTokenAsync(string refreshToken);
        Task AddUserAsync(App.Domain.Entites.User user);
        void UpdateAsync(App.Domain.Entites.User user);
       
    }
}
