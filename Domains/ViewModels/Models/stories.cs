using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.ViewModels.Models
{
    [BsonIgnoreExtraElements]
    public class stories
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB's unique identifier

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("mediaType")]
        public string MediaType { get; set; } = string.Empty;

        [BsonElement("story")]
        public List<string> Stories { get; set; } = new List<string>(); // Ensure uniqueness in MongoDB with an index

        [BsonElement("time")]
        public DateTime AddedTime { get; set; }

        [BsonElement("views")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<ObjectId> Following { get; set; } = new List<ObjectId>();
        public List<users> StoryUser { get; set; } = new List<users>();
    }
}
