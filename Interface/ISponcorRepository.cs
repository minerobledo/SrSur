using SrSur.Models.DTOs.Imput.Sponsor;
using SrSur.Models.Entities;

namespace SrSur.Interface
{
    public interface ISponcorRepository
    {
        Task CreateSponsor(Sponsor dto);
        Task UpdateSponsor(Sponsor dto);
        Task<bool> DeleteSponsor( long id );
        Task<List<Sponsor>> GetSponsors();
        Task<Sponsor?> GetSponsorById(long id);
    }
}
