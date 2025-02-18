using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Register;
public class RegisterUserUseCases
{
    public ResponseRegisterUserJson Execute(RequestUserJson request)
    {
        Validate(request);

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password
        };

        var dbContext = new TechLibraryDbContext();

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();


        return new ResponseRegisterUserJson
        {
            Name = entity.Name
        };
    }

    private void Validate(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errosMenssages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errosMenssages);
        }

    }
}
