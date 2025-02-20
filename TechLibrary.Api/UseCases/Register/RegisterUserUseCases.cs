using FluentValidation.Results;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Infraestructure.Security.Cryptography;
using TechLibrary.Api.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Register;
public class RegisterUserUseCases
{
    public ResponseRegisterUserJson Execute(RequestUserJson request)
    {

        var dbContext = new TechLibraryDbContext();

        Validate(request, dbContext);

        var cryptography = new BcryptAlgorithm();

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = cryptography.HashPassword(request.Password)
        };

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        var tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisterUserJson
        {
            Name = entity.Name,
            AcessToken = tokenGenerator.Generate(entity)
        };
    }

    private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var existUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email));

        if (existUserWithEmail)
            result.Errors.Add(new ValidationFailure("Email", "E-mail já registrado na plataforma"));

        if (!result.IsValid)
        {
            var errosMenssages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errosMenssages);
        }

    }
}
