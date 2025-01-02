using Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPostRepository
    {
        List<FetchedPostsViewModel> FetchPosts(int limit, string userId);
    }
}
