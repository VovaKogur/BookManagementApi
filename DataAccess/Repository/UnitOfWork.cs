using ASP.DataAccess.Repository.IRepository;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace ASP.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IBookRepository Book { get; private set; }
        public IReviewRepository Review {  get; private set; }

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            Book = new BookRepository(db);
            Review=new ReviewRepository(db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
