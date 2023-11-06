using ASP.DataAccess.Repository.IRepository;
using Models.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        Review GetById(int id);
    }
}
