using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(long id);
        Task<List<Comment>?> GetAllAsync();
        Task<Comment?> GetByIdAsync(long id);

        Task<List<Comment>> GetPaginatedAsync(int pageNumber, int pageSize, long? idPost = null);
        Task<List<Comment>> GetPaginatedParentAsync(long parentID, int pageNumber, int pageSize);
    }
}
