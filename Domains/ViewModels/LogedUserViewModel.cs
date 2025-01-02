using Domains.ViewModels.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.ViewModels
{
    public class LogedUserViewModel
    {
        public users LogedUser { get; set; } = new users();
        public List<users> StoryUsers { get; set; }=new List<users>();
        public int TotalSotries { get; set; }
    }
}
