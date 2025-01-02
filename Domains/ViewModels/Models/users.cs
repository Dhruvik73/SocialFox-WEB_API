using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domains.ViewModels.Models
{
    [BsonIgnoreExtraElements]
    public class users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB's unique identifier

        [BsonElement("firstname")]
        public string FirstName { get; set; }

        [BsonElement("lastname")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; } // Ensure uniqueness in MongoDB with an index

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("profilephoto")]
        [BsonDefaultValue("")]
        public string ProfilePhoto { get; set; } = string.Empty;

        [BsonElement("followers")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Followers { get; set; } = new List<string>();

        [BsonElement("following")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Following { get; set; } = new List<string>();
    }
}
