using FluentValidation;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace Hahn.ApplicationProcess.December2020.Domain.Commands
{
    public record ApplicantEditCommand(
        string Name,
        string FamilyName,
        int Age,
        string EmailAddress,
        string Address,
        string CountryOfOrigin,
        bool Hired
    ) : IRequest
    {
        [JsonIgnore]
        public int ApplicantId { get; set; }
    };

    public class ApplicantEditCommandValidator : AbstractValidator<ApplicantEditCommand>
    {
        public ApplicantEditCommandValidator(ICountryRepository repository)
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(v => v.FamilyName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(v => v.Address)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(256);

            RuleFor(v => v.EmailAddress)
                .NotEmpty()
                .EmailAddress();

            RuleFor(v => v.Age)
                .NotEmpty()
                .GreaterThan(19)
                .LessThan(61);

            RuleFor(v => v.CountryOfOrigin)
                .NotEmpty()
                .ValidCountryName(repository);
        }
    }
}