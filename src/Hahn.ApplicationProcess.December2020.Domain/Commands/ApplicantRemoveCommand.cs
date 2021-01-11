using MediatR;

namespace Hahn.ApplicationProcess.December2020.Domain.Commands
{
    public record ApplicantRemoveCommand(int ApplicantId) : IRequest;
}