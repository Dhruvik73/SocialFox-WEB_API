using Domains.ViewModels;
using Domains.ViewModels.Models;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<LogedUserViewModel> GetLogedUserDetails(string logedUserId)
        {
            return await userRepository.GetLogedUserDetails(logedUserId);
        }

        public List<users> GetSuggestedAllies(string logedUserId)
        {
            return userRepository.GetSuggestedAllies(logedUserId);
        }
    }
}
