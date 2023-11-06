using ASP.DataAccess.Repository.IRepository;
using DataAccess.Data;

namespace ASP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IBookRepository Book { get; private set; }

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            Book = new BookRepository(db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
