using Domains.ViewModels;
using Domains.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserServices
    {
        Task<LogedUserViewModel> GetLogedUserDetails(string logedUserId);
        List<users> GetSuggestedAllies(string logedUserId);
    }
}
