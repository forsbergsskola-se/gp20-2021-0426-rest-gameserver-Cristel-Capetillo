using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace LameScooter {
    public class MongoDbLameScooterRental: ILameScooterRental {
        public async Task<int> GetScooterCountInStation(string stationName) {
            
            if (stationName.Any(char.IsDigit)) {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("The station name cannot contain a number");
            }
            
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("lamescooters");
            var collection = database.GetCollection<BsonDocument>("lamescooters");
            AddConventionPacks();
            var filter = Builders<BsonDocument>.Filter.Eq("name", stationName);

            BsonDocument document;
            try {
                document = collection.Find(filter).First();
            }
            catch {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new NotFoundException($"Could not find the station: {stationName}");
            }
            
            var result = BsonSerializer.Deserialize<LameScooterStationList>(document);
            return result.BikesAvailable;
        }

        static void AddConventionPacks() {
            var conventionPack = new ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            
            conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);
        }
    }
}