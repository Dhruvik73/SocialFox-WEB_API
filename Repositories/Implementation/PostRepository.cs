using Domains.Data;
using Domains.ViewModels;
using Domains.ViewModels.Models;
using MongoDB.Driver;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<posts> _collection;

        public PostRepository(MongoDbContext dbContext)
        {
            _collection = dbContext.GetMongoCollection<posts>("posts");
        }
        public List<FetchedPostsViewModel> FetchPosts(int limit, string userId)
        {
            var posts = _collection.AsQueryable().ToList();
            return new List<FetchedPostsViewModel>();
        }
    }
}
