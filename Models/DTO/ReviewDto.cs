namespace Models.DTO
{
    public class ReviewDTO
    {
        public string ReviewerEmail { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
