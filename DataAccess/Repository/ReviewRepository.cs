using ASP.DataAccess.Repository;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.Models;

namespace DataAccess.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public Review GetById(int id)
        {
            var review=_db.Reviews.FirstOrDefault(x => x.Id == id);
            return review;
        }
    }
}
