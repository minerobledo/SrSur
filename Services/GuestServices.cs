using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.Entities;
using SrSur.Repositories;

namespace SrSur.Services
{
    public class GuestServices
    {
        private readonly IGuestRepository _guestRepository;

        public GuestServices(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<Responce<long>> CreateGuset(string userAgent,string ipAdres, string referer, string country, string region)
        {
            var g= await _guestRepository.GetByIPAsync(ipAdres);
            if (g == null)
            {
                var guest = new Guest()
                {
                    Useragent = userAgent,
                    Ipaddress = ipAdres,
                    Referrer = referer,

                    Country = country,
                    Region = region
                };
                await _guestRepository.AddAsync(guest);
                g = await _guestRepository.GetByIPAsync(ipAdres);
                return new Responce<long>() { data = g.Id, succeeded = true };
            }
            return new Responce<long>() { data = g.Id, succeeded = true, message = "ya existe esta ip" };
        }
       

        public async Task<Responce<bool>> DeleteGuest (long id)
        {
            var tag = await _guestRepository.GetByIdAsync(id);
            if (tag != null)
            {
                await _guestRepository.DeleteAsync(id);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        
    }
}
