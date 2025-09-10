namespace SrSur.Models.DTOs.Imput.Post
{
    public class CreatePostDto: DTOGeneric 
    {
        public long AuthorId { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }

        public string? CommentStatus { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string? PostStatus { get; set; } // Pending , Publish

        public string? Excerpt { get; set; }
    }
}
