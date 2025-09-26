namespace SrSur.Models.DTOs.Imput.Sponsor
{
    public class CreateSponsorDto
    {
        
        public string Name { get; set; }
        public double Amount { get; set; }   // o decimal si es dinero
        public IFormFile FormFile { get; set; }
        public bool Paid { get; set; }
    }
    public class UpdateSponsorDto
    {
        public long id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }   // o decimal si es dinero
    }
    
}
