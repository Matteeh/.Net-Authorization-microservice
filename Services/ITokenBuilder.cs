using Authorization.Models;

namespace Authorization.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(ApplicationUser appUser);
    }
}