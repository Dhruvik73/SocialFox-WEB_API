using Domains.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.ViewModels
{
    public class FetchedPostsViewModel
    {
        public string? id { get; set; }
        public users user { get; set; }=new users();
        public List<string> post { get; set; } = new List<string>();
        public List<string> like { get; set; } = new List<string>();
        public List<string> dislike { get; set; } = new List<string>();
        public string? description { get; set; }
        public List<string> bgColor { get; set; } = new List<string>();
        public DateTime? insertDate { get; set; }
        public int userPostCount { get; set; }
        public int commentsCount { get; set; }
    }
}
