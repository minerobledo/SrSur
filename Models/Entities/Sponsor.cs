namespace SrSur.Models.Entities
{
    public class Sponsor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime LastPayment { get; set; }
        public bool Paid { get; set; }
        public virtual ICollection<SponsorImage> SponsorImages { get; set; } = new List<SponsorImage>();

    }
}
