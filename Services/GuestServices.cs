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

        public async Task<Responce<bool>> CreateGuset(string userAgent,string ipAdres, string referer, string country, string region)
        {
            Console.WriteLine("el ipadres es:"+ipAdres);
            var guest = new Guest()
            {
                Useragent = userAgent,
                Ipaddress = ipAdres,
                Referrer = referer,
                
                Country = country,
                Region = region
            };
            await _guestRepository.AddAsync(guest);
            return new Responce<bool>() { data = true, succeeded = true };
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
