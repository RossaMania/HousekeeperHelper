using System.Linq;

namespace HousekeeperHelperProject.Mocking
{
    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            // Implementation for querying the database
            return new List<T>().AsQueryable(); // Placeholder implementation
        }
    }
}