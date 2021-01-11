using FluentValidation;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;

namespace Hahn.ApplicationProcess.December2020.Domain.Commands
{
    public static class CountryNameValidator
    {
        public static IRuleBuilderOptions<T, string> ValidCountryName<T>
            (this IRuleBuilder<T, string> ruleBuilder, ICountryRepository repository)
        {
            return ruleBuilder.Must(name => repository.CheckCountryExistByNameAsync(name).GetAwaiter().GetResult())
                .WithMessage("Country name is not valid");
        }
    }
}