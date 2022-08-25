using BuySubs.BLL.Commands.Sites;
using FluentValidation;

namespace BuySubs.API.Validators.Sites;

public sealed class GetAllSitesCommandValidator : AbstractValidator<GetAllSitesCommand>
{
    public GetAllSitesCommandValidator() { }
}