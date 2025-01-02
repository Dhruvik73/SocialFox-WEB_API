using Domains.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using Repositories.Interfaces;

namespace Repositories.Implementation
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(MongoDbContext context)
        {
            _collection = context.GetMongoCollection<T>(typeof(T).Name);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetDataByField(string fieldName, string fieldValue)
        {
            var filter = Builders<T>.Filter.Eq(fieldName, fieldValue);
            if (fieldName == "_id")
            {
                filter= Builders<T>.Filter.Eq(fieldName, ObjectId.Parse(fieldValue));
            }
            return await _collection.Find(filter).ToListAsync();
        }
    }
}
