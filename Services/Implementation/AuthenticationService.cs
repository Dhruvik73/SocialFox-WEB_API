using Domains.ViewModels;
using Domains.ViewModels.Models;
using Repositories.Implementation;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthentication authentication;
        public AuthenticationService(IAuthentication authentication)
        {
            this.authentication = authentication;
        }

        public async Task<AuthValidationModel> CreateUser(users user)
        {
            return await authentication.CreateUser(user);
        }

        public AuthValidationModel VerifyToken(string token)
        {
            return authentication.VerifyToken(token);
        }

        public async Task<AuthValidationModel> VerifyUser(string userEmail, string password)
        {
            return await authentication.VerifyUser(userEmail, password);
        }
    }
}
