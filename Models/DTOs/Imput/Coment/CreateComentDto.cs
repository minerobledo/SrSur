namespace SrSur.Models.DTOs.Imput.Coment
{
    public class CreateComentDto : DTOGeneric
    {
        public long PostId { get; set; }

        public long? UserId { get; set; } = null!;

        public long? ParentId { get; set; } = null!;

        public string? Author { get; set; }

        public string EmailAuthor { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string Device { get; set; } = null!;

        public string? Country { get; set; }

        public string? Region { get; set; }

        public string? AuthorUrl { get; set; }
    }
    public class EditComentDto : DTOGeneric
    {
        public long Id { get; set; }
        public string? Body { get; set; }
    }
    public class GetComentByPostDto : DTOGeneric
    {
        public int Page { get; set; }
        public long PostId { get; set; }
    }
    public class GetComentParentsByPostDto : DTOGeneric
    {
        public int Page { get; set; }
        public long CommentParent { get; set; }
    }
}

