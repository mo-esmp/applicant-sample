using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain.Repositories
{
    /// <summary>
    /// Country data repository
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Checks the country exist by name asynchronous.
        /// </summary>
        /// <param name="name">The name of country.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> CheckCountryExistByNameAsync(string name);
    }
}