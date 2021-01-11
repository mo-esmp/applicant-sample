using Hahn.ApplicationProcess.December2020.Domain.Entities;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain.Repositories
{
    /// <summary>
    /// Applicant data repository
    /// </summary>
    public interface IApplicantRepository
    {
        /// <summary>
        /// Adds the applicant asynchronous.
        /// </summary>
        /// <param name="applicant">The applicant.</param>
        /// <returns>Task.</returns>
        Task AddAsync(ApplicantEntity applicant);

        /// <summary>
        /// Edits the applicant asynchronous.
        /// </summary>
        /// <param name="applicant">The applicant.</param>
        /// <returns>Task.</returns>
        Task EditAsync(ApplicantEntity applicant);

        /// <summary>
        /// Removes the applicant asynchronous.
        /// </summary>
        /// <param name="id">The applicant identifier.</param>
        /// <returns>Task.</returns>
        Task RemoveAsync(int id);

        /// <summary>
        /// Removes the applicant asynchronous.
        /// </summary>
        /// <param name="applicant">The applicant.</param>
        /// <returns>Task.</returns>
        Task RemoveAsync(ApplicantEntity applicant);

        /// <summary>
        /// Gets the applicant by identifier asynchronous.
        /// </summary>
        /// <param name="id">The applicant identifier.</param>
        /// <returns>ValueTask&lt;ApplicantEntity&gt;.</returns>
        ValueTask<ApplicantEntity> GetByIdAsync(int id);
    }
}