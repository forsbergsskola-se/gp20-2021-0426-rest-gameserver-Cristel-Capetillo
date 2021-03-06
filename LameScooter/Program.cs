using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter {
    class Program {
        static async Task Main(string[] args) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LameScooter is loading information...");
            OfflineLameScooterRental rental = new OfflineLameScooterRental();
            var count = await rental.GetScooterCountInStation(stationName:"Luhtimäki");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Number of scooters available at Luhtimäki station: " + count);
        }
    }

    public interface ILameScooterRental {
        Task<int> GetScooterCountInStation(string stationName);
    }

    public class OfflineLameScooterRental : ILameScooterRental {
        public async Task<int> GetScooterCountInStation(string stationName) {
            var file = await File.ReadAllTextAsync("Scooters.json");

            var convertToJson = JsonConvert.DeserializeObject<LameScooterStationList>(file);

            if (convertToJson != null)
                foreach (var station in convertToJson.Stations) {
                    if (station.name == stationName) {
                        return station.bikesAvailable;
                    }
                }
            return default;
        }

        public class LameScooterStationList {
            public List <LameScooterStation> Stations;
        }
        
        public class LameScooterStation {
            public string name { get; set; }
            public int bikesAvailable { get; set; }
        }
    }
}
