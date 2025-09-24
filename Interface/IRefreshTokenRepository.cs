using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(Refreshtoken token);
        Task DeletaAsync(long id);
        Task<List<Refreshtoken>?> GetAllAsync();
        Task<Refreshtoken?> GetByIdAsync(long id);
        Task<Refreshtoken?> GetByRefreshTokenAsync(string token);
        Task<Refreshtoken?> GetByUserIdAsync(long id);
        Task UpdateAsync(Refreshtoken token);
    }
}
