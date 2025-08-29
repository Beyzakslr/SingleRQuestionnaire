using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Concurrent;
namespace SingleRQuestionnaire.Models
{
    public class Poll
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Soru")]
        public string Question { get; set; } = string.Empty;

        [BsonElement("Secenekler")]
        public Dictionary<string, List<string>> Options { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, int> GetVoteCounts()
        {
            return Options.ToDictionary(k => k.Key, k => k.Value.Count);
        }
    }
}
