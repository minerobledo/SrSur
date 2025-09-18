namespace SrSur.Models.DTOs.Imput.Post
{
    public class EditPostDto : DTOGeneric
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }

        public string? CommentStatus { get; set; }

        

        public string? PostStatus { get; set; } // Pending , Publish

        public string? Excerpt { get; set; }

        public long? CategoryId { get; set; }

        public List<long?>? TagsId { get; set; }
    }
}
