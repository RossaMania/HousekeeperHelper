using System.Linq;

namespace HousekeeperHelperProject.Mocking
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }
}