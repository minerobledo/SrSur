namespace SrSur.Models.DTOs.Imput.PostVisitedByGuset
{
    public class CreatePostVisitedByGusetDto:DTOGeneric
    {
        public long Postid { get; set; }

        public long Guestid { get; set; }

        public int Visitetime { get; set; }

    }
}
