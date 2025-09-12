using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface IPostsVisitedByGuestRepository
    {
        Task AddAsync(Guest guest);
        Task UpdateAsync(Guest guest);
        Task DeleteAsync(long id);
    }
}
