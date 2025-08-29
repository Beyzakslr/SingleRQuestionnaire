using MongoDB.Driver;
using SingleRQuestionnaire.Models;

namespace SingleRQuestionnaire.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Poll> _pollsCollection;

        public MongoDbService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDbSettings:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value;
            var collectionName = configuration["MongoDbSettings:CollectionName"];
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentException("MongoDbSettings:CollectionName is missing in configuration.");

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _pollsCollection = mongoDatabase.GetCollection<Poll>(collectionName);
        }

        public async Task<Poll?> GetAsync(string id) =>
            await _pollsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Poll newPoll) =>
            await _pollsCollection.InsertOneAsync(newPoll);
        public async Task IncrementVoteAsync(string pollId, string option)
        {
            var filter = Builders<Poll>.Filter.Eq(p => p.Id, pollId);
            var update = Builders<Poll>.Update.Inc($"Secenekler.{option}", 1);

            await _pollsCollection.UpdateOneAsync(filter, update);
        }

        public async Task<bool> VoteAsync(string pollId, string option, string userId)
        {
            var poll = await GetAsync(pollId);
            if (poll == null) return false;

            if (!poll.Options.ContainsKey(option))
                poll.Options[option] = new List<string>();

            bool hasVoted = poll.Options.Values.Any(voters => voters.Contains(userId));

            if (hasVoted)
            {
                throw new Exception("Kullanıcı zaten oy vermiş.");
            }

            var filter = Builders<Poll>.Filter.Eq(p => p.Id, pollId);

            var update = Builders<Poll>.Update.Push($"Options.{option}", userId);

            var result = await _pollsCollection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }
    }
}
