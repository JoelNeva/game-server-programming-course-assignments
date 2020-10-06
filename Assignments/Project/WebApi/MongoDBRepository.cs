using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Newtonsoft.Json;

public class MongoDBRepository : IRepository
{
    private readonly IMongoCollection<Listing> _listingCollection;
    private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;
    public MongoDBRepository()
    {
        MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("arkanoid");
        _listingCollection = database.GetCollection<Listing>("highScores");
        _bsonDocumentCollection = database.GetCollection<BsonDocument>("highScores");
    }

    public async Task<Listing> Delete(Guid id)
    {
        var filter = Builders<Listing>.Filter.Eq(p => p.id, id);
        Listing listing = await _listingCollection.Find(filter).FirstOrDefaultAsync();
        await _listingCollection.DeleteOneAsync(filter);
        return listing;
    }

    public async Task<Listing[]> GetAll()
    {
        var filter = Builders<Listing>.Filter.Empty;
        List<Listing> highScores = await _listingCollection.Find(filter).ToListAsync();
        return highScores.ToArray();
    }

    public async Task<Listing> NewHighScore(Listing newListing)
    {
        var filter = Builders<Listing>.Filter.Eq(p => p.id, newListing.id);
        Listing listing = await _listingCollection.Find(filter).FirstOrDefaultAsync();
        if (listing != null)
        {
            if (newListing.Score > listing.Score)
            {
                await _listingCollection.ReplaceOneAsync(filter, newListing);
            }
        }
        else
        {
            await _listingCollection.InsertOneAsync(newListing);
        }
        return await _listingCollection.Find(filter).FirstOrDefaultAsync();
    }
}