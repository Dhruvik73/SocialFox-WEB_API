using Domains.ViewModels;
using Domains.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<LogedUserViewModel> GetLogedUserDetails(string logedUserId);
        List<users> GetSuggestedAllies(string logedUserId);
    }
}
