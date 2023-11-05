using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagementApi.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        public string ReviewerEmail { get; set; }
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
