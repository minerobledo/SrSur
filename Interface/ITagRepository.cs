using SrSur.Models.DTOs.output;
using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface ITagRepository
    {
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(long id);
        Task<List<Tag>?> GetAllAsync();
        Task<List<Tag>> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<Tag?> GetByIdAsync(long id);
        Task<Tag?> GetByNameAsync(string name);
        Task<List<Tag>> GetByPrefixAsync(string prefix, int pageNumber, int pageSize);
        List<TagOutDto> TransformList(List<Tag> list);
    }
}
