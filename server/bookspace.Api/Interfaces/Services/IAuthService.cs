using bookspace.Api.Entities;

namespace bookspace.Api.Services
{
    public interface IAuthService
    {
        string generateJwtToken(User user);
        public bool VerifyPassword(string hash, string password);
    }
}