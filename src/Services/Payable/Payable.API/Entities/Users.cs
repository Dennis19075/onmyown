using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Payable.API.Entities
{
	public class Users
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("first_name")]
        public string firstName { get; set; }

        [BsonElement("last_name")]
        public string lastName { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("phone")]
        public string phone { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("created_at")]
        public DateTime createdAt { get; set; }

        [BsonElement("edited_at")]
        public DateTime editedAt { get; set; }    

	}
}

