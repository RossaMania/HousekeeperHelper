using System.Linq;

namespace HousekeeperServiceProject.Mocking
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }
}