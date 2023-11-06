namespace Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; } 
        public CategoryDTO Category { get; set; }
        public float AverageRating { get; set; } 
    }

}

