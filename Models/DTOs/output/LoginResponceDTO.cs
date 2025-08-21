namespace SrSur.Models.DTOs.output
{
    public class LoginResponceDTO
    {
        
        public string? JWT{ get; set; }
        public string? RefreshToken{ get; set; }
        public long UserId{ get; set; }
    
    }
}
