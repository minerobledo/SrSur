using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(long id);
        Task<List<Post>?> GetAllAsync();
        Task<List<Post>> SerchByFilterAsync(Dictionary<string, object> filters, int pageNumber, int pageSize);
        Task<Post?> GetByIdAsync(long id);
        Task<List<Post?>> SearchPostWithNeighbors(Dictionary<string, object> filters);
    }
}
