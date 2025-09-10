using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(long id);
        Task<List<Tag>?> GetAllAsync();
        Task<Tag?> GetByIdAsync(long id);
        Task<Tag?> GetByNameAsync(string name);
    }
}
