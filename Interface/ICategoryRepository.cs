using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(long id);
        Task<List<Category>?> GetAllAsync();
        Task<Category?> GetByIdAsync(long id);
        Task<Category?> GetByNameAsync(string name);

        Task<List<Category>> GetByPrefixAsync(string prefix, int pageNumber, int pageSize);
        Task<List<Category>> GetPaginatedAsync(int pageNumber, int pageSize);

    }
}
