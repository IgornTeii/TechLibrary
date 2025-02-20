using BCrypt.Net;

namespace TechLibrary.Api.Infraestructure.Security.Cryptography;

public class BcryptAlgorithm
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
}
