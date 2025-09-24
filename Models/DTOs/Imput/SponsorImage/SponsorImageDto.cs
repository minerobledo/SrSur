namespace SrSur.Models.DTOs.Imput.SponsorImage
{
    public class CreateSponsorImageDto : DTOGeneric
    {
        public long SponsorID {  get; set; }
        public required IFormFile FormFile { get; set; }
    }
    public class EditSponsorImageDto:DTOGeneric
    {
        public long imageId { get; set; }
        public required IFormFile FormFile { get; set; }
    }
}
