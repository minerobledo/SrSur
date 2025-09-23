namespace SrSur.Models.DTOs.Imput.resourse
{
    public class CreateResourseDto : DTOGeneric
    {
        public string? Titel { get; set; }
        public IFormFile? FormFile { get; set; }
    }
    public class EditResourseDto : DTOGeneric
    {
        public string? Titel { get; set; }
        public long Id { get; set; }
    }
    public class GetResourseDto : DTOGeneric
    {
        public string? Titel { get; set; }
        public int Page { get; set; }
    }
}
   