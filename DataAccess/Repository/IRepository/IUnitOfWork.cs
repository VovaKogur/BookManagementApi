using DataAccess.Repository.IRepository;

namespace ASP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IBookRepository Book { get; }
        IReviewRepository Review { get; }
        void Save();

    }
}
