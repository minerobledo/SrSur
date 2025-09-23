using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface IResourseRepocitory
    {
        Task CreateResorse(Resourse resourse);
        Task<bool> DeleteResorse(long Id);
        Task EditResourse(Resourse resourse);
        Task<Resourse?> GetResorseByID(long id);
        Task<List<Resourse>> GetResourseList(string title, int pageNumber, int pageSize);
    }
}
