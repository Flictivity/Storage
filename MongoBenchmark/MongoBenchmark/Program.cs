// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<MongoDbBenchmarks>();
        Console.ReadLine();
    }
}


public class MongoDbBenchmarks
{
    private IMongoCollection<User> collection;
    [GlobalSetup]
    public void Setup()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var db = client.GetDatabase("CompServiceDb");
        collection = db.GetCollection<User>("Users");
    }
    [Benchmark]
    public void Find()
    {
        collection.Find(x => true);
    }

    [Benchmark]
    public async Task FindAsync()
    {
        await collection.FindAsync(x => true);
    }
}

class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
}