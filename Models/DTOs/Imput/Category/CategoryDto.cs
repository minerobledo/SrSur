namespace SrSur.Models.DTOs.Imput.Category
{
    public class CategoryDto: DTOGeneric
    {
        public long? ParentId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Slug { get; set; }

        public string? CategoryDescription { get; set; }
    }
    public class EditCategoryDto : DTOGeneric
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Slug { get; set; }

        public string? CategoryDescription { get; set; }
    }
}
