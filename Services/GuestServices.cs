using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.Entities;

namespace SrSur.Services
{
    public class GuestServices
    {
        private readonly IGuestRepository _guestRepository;

        public GuestServices(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<Responce<bool>> CreateGuset(string userAgent,string ipAdres, string referer, string device, string country, string region)
        {
            var guest = new Guest()
            {
                UserAgent = userAgent,
                IpAddress = ipAdres,
                Referrer = referer,
                Device = device,
                Country = country,
                Region = region
            };
            await _guestRepository.AddAsync(guest);
            return new Responce<bool>() { data = true, succeeded = true };
        }

    }
}
