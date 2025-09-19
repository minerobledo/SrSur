using SrSur.Services;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace SrSur.Models.DTOs.Imput.Guest
{
    public class CreateGuestDto: DTOGeneric 
    {
        public string? Useragent { get; set; }

        public string Ipaddress { get; set; } = null!;

        public string? Referrer { get; set; }

        
        
        public string? Country { get; set; }

        public string? Region { get; set; }
    }
}
