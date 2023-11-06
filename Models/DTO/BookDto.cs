using Microsoft.AspNetCore.Http;

namespace Models.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public IFormFile PdfFile { get; set; }
        public int CategoryId { get; set; }
    }

}

