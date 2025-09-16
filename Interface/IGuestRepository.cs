using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface IGuestRepository
    {
        Task AddAsync(Guest guest);
        Task UpdateAsync(Guest guest);
        Task DeleteAsync(long id);
        Task<List<Guest>?> GetAllAsync();
        Task<Guest?> GetByIdAsync(long id);


    }
}
