namespace SrSur.Models.DTOs.Imput.Tag
{
    public class TagDto: DTOGeneric
    {
        public string? TagName { get; set; }

        public string? Slug { get; set; }

        public string? TagDescription { get; set; }
    }
    public class EditTagDto : DTOGeneric
    {
        public long TagId { get; set; }
        public string? TagName { get; set; }

        public string? Slug { get; set; }

        public string? TagDescription { get; set; }
    }
    public class SerchTagDto : DTOGeneric
    {
        public int Page { get; set; }
        public string? Prefix { get; set; }

      
    }
}
