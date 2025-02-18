using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.UseCases.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("O nome não pode ser vazio!");
        RuleFor(request => request.Email).EmailAddress().WithMessage("O email não é valido!");
        When(request => string.IsNullOrEmpty(request.Password) == false, () =>
        {
            RuleFor(request => request.Password.Length).GreaterThan(6).WithMessage("A senha tem que ter mais de 6 caracteres.");
        });
    }
}
