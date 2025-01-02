using Domains.ViewModels;
using Domains.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthValidationModel> VerifyUser(string userEmail, string password);
        Task<AuthValidationModel> CreateUser(users user);
        AuthValidationModel VerifyToken(string token);
    }
}
