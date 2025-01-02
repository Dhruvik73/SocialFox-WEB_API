using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domains.ViewModels
{
    [BsonIgnoreExtraElements]
    public class chats
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB's unique identifier

        [BsonElement("fromUser")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string FromUser { get; set; } = string.Empty;

        [BsonElement("toUser")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string ToUser { get; set; } = string.Empty;

        [BsonElement("chats")]
        [BsonRequired]
        public List<object> UserChats { get; set; } = new List<object>(); // Ensure uniqueness in MongoDB with an index

        [BsonElement("chatUpdateDate")]
        public DateTime ChatUpdateDate { get; set; }

        [BsonElement("chatStartDate")]
        public DateTime ChatStartDate { get; set; } 
    }
}
