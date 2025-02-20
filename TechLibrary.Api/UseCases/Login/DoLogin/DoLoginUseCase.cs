using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.UseCases.Login.DoLogin;

public class DoLoginUseCase
{
    public ResponseRegisterUserJson Execute(RequestLoginJson request)
    {
        var dbContext = new TechLibraryDbContext();

        var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));

        return new ResponseRegisterUserJson
        {

        };
    }
}
