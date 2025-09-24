using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface ISponsorImageRepocitory
    {
        Task CreateAsync(SponsorImage image);
        Task<bool> DeleteAsync(long id);
        Task<SponsorImage?> GetAsync(long id);
        Task<List<SponsorImage>> GetAllPagitatedAsync(int pageNumber, int pageSize);
        Task<List<SponsorImage>> GetRandomAsync(int count);
        Task UpdateSponsor(SponsorImage dto);
    }
}
