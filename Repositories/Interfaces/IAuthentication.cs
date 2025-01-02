using Domains.ViewModels;
using Domains.ViewModels.Models;

namespace Repositories.Interfaces
{
    public interface IAuthentication
    {
        Task<AuthValidationModel> VerifyUser(string userEmail, string password);
        Task<AuthValidationModel> CreateUser(users user);
        AuthValidationModel VerifyToken(string token);
    }
}
