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
    public class posts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB's unique identifier
        [BsonElement("user")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string postUser { get; set; }

        [BsonElement("post")]
        public List<string> Posts { get; set; }=new List<string>();

        [BsonElement("like")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Likes { get; set; } = new List<string>();

        [BsonElement("dislike")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> DisLikes { get; set; } = new List<string>();

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("bgColor")]
        public List<string> BgColor { get; set; } = new List<string>();

        [BsonElement("insertDate")]
        public DateTime InsertDate { get; set; }

        [BsonElement("userPostCount")]
        public int UserPostCount { get; set; }
    }
}
