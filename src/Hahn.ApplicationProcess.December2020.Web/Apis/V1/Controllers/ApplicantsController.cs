using Hahn.ApplicationProcess.December2020.Domain;
using Hahn.ApplicationProcess.December2020.Domain.Commands;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Queries;
using Hahn.ApplicationProcess.December2020.Web.Infrastructure.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Apis.V1.Controllers
{
    [ApiController, ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApplicantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicantsController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///   Gets the list of applicants
        /// </summary>
        /// <response code="200">List of applicants</response>
        [HttpGet]
        public async Task<IEnumerable<ApplicantEntity>> Get()
        {
            return await _mediator.Send(new ApplicantGetsQuery());
        }

        /// <summary>
        ///   Gets the applicant information by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">The applicant</response>
        /// <response code="204">If the applicant not found</response>
        [HttpGet("{id:int}")]
        public async Task<ApplicantEntity> Get(int id)
        {
            return await _mediator.Send(new ApplicantGetQuery(id));
        }

        /// <summary>
        ///   Adds new applicant information
        /// </summary>
        /// <param name="command"></param>
        /// <param name="version"></param>
        /// <response code="201">If the operation is successful</response>
        /// <response code="400">If the posted body is not valid</response>
        [HttpPost]
        [SwaggerRequestExample(typeof(ApplicantAddCommand), typeof(ApplicantCommandExampleValue))]
        public async Task<IActionResult> Post([FromBody] ApplicantAddCommand command, ApiVersion version)
        {
            var id = await _mediator.Send(command);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(Get), new { id, Version = $"{version}" }, new ApplicantEntity { Id = id });
        }

        /// <summary>
        ///   Edits an applicant information
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <response code="200">If the operation is successful</response>
        /// <response code="400">If the posted body is not valid or applicant could not be found</response>
        [HttpPut("{id:int}")]
        [SwaggerRequestExample(typeof(ApplicantAddCommand), typeof(ApplicantCommandExampleValue))]
        public async Task<IActionResult> Put(int id, [FromBody] ApplicantEditCommand command)
        {
            command.ApplicantId = id;
            await _mediator.Send(command);
            await _unitOfWork.CommitAsync();

            return Ok();
        }

        /// <summary>
        ///   Removes an applicant
        /// </summary>
        /// <param name="id">Applicant identifier</param>
        /// <response code="200">If the operation is successful</response>
        /// <response code="400">If the applicant could not be found</response>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new ApplicantRemoveCommand(id));
            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}