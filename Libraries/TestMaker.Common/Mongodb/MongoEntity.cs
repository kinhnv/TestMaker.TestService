﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestMaker.Common.Mongodb
{
    public abstract class MongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    }
}
