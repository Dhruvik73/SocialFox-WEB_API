using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.ViewModels;
using Domains.ViewModels.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Domains.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoDB");
            var client = new MongoClient(settings["ConnectionString"]);
            _db = client.GetDatabase(settings["DataBase"]);
            EnsureIndexes();
        }
        private void EnsureIndexes()
        {
            // Example: Ensure unique index on "Email" for "users" collection
            var collection = _db.GetCollection<users>("users");
            var userIndexs = collection.Indexes.List().ToList();
            if (userIndexs.Count < 2)
            {
                var indexOptions = new CreateIndexOptions { Unique = true };
                var indexKeys = Builders<users>.IndexKeys.Ascending(x => x.Email);
                var indexModel = new CreateIndexModel<users>(indexKeys, indexOptions);
                collection.Indexes.CreateOne(indexModel);
            }

            //Created expire after index from stories
            var storyCollection = _db.GetCollection<stories>("stories");
            var sotryIndexs=storyCollection.Indexes.List().ToList();
            if (sotryIndexs.Count < 2)
            {
                var storyIndexOptions = new CreateIndexOptions { ExpireAfter = new TimeSpan(1, 0, 0, 0) };
                var storyIndexKeys = Builders<stories>.IndexKeys.Ascending(x => x.AddedTime);
                var soryIndexModel = new CreateIndexModel<stories>(storyIndexKeys, storyIndexOptions);
                storyCollection.Indexes.CreateOne(soryIndexModel);
            }
        }
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
        {
            return _db.GetCollection<T>(collectionName);
        }
    }
}
