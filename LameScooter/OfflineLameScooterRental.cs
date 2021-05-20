using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    class DeprecatedLameScooterRental : ILameScooterRental {
        public Task<int> GetScooterCountInStation(string stationName) {
            throw new NotImplementedException();
        }
    }
    public class OfflineLameScooterRental : ILameScooterRental {
        public string path = "scooters.json";

        public async Task<int> GetScooterCountInStation(string stationName) {
            using var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, path));
            var file = JsonSerializer.Deserialize<LameScooterStationList>(await sr.ReadToEndAsync());

            foreach (var station in file.stations) {
                if (stationName == station.name) {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{station.name} station has these amount of available scooters:");
                    return station.bikesAvailable;
                }
            }
            var notFoundException = new Exception("Cannot find the station: " + stationName);
            throw notFoundException;
        }
    }
}