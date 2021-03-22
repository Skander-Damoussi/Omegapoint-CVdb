using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cv_api.Models;

namespace cv_api
{
    public class repository
    {
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }
        public repository()
        {
            Client = new MongoClient("mongodb+srv://Admin:Test123@cvdb.wonwu.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            Database = Client.GetDatabase("TestDB");
        }

        public List<BsonDocument> Get(string collection)
        {
            var col = Database.GetCollection<BsonDocument>(collection);
            return col.Find(new BsonDocument()).ToList();
        }

        public bool Update(string collection, BsonDocument find, string field, string update)
        {
            var filter = find;
            var updateThis = Builders<BsonDocument>.Update.Set(field, update);
            
            var col = Database.GetCollection<BsonDocument>(collection);
            var result = col.FindOneAndUpdate(filter, updateThis);

            return true;
        }

        public bool Post<T>(string collection, T post)
        {
            var col = Database.GetCollection<T>(collection);
            col.InsertOne(post);
            return true;
        }
    }
}
