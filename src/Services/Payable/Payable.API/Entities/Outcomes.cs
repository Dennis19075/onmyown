using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Payable.API.Entities
{
	public class Outcomes
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("created_at")]
        public DateTime createdAt { get; set; }

        [BsonElement("edited_at")]
        public DateTime editedAt { get; set; }

        [BsonElement("description")]
        public string description { get; set; }

        [BsonElement("category")]
        public string category { get; set; }

        [BsonElement("expense")]
        public double expense { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }

        public Outcomes()
		{
		}
	}
}

