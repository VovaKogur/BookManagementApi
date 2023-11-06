using Models.Models;

namespace ASP.DataAccess.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book book);
        void RemoveById(int id);
        Book FindById(int id);
    }
}
