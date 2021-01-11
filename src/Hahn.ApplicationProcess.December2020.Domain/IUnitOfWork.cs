using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain
{
    /// <summary>
    /// UnitOfWork interface
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits all changes made to entities in current request to database asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The task result contains the number of state entries written to the database.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}