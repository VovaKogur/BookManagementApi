using ASP.DataAccess.Repository.IRepository;
using DataAccess.Data;
using Models.Models;

namespace ASP.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public Book FindById(int id)
        {
            var book=_db.Books.FirstOrDefault(x => x.Id == id);
            return book;
        }
        public void RemoveById(int id)
        {
            var book = _db.Books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                _db.Books.Remove(book);
            }
        }

        public void Update(Book book)
        {
            _db.Books.Update(book);
        }

       
    }
}
