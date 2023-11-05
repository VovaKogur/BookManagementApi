using System.ComponentModel.DataAnnotations;

namespace BookManagementApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
