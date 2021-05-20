﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class RealTimeLameScooterRental: ILameScooterRental {
        public async Task<int> GetScooterCountInStation(string stationName) {
            if (stationName.Any(char.IsDigit)) {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("The station name cannot contain a number");
            }
  
            const string uri = "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
            var serverTextjson = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            using(var response = (HttpWebResponse)await request.GetResponseAsync())
            await using(var stream = response.GetResponseStream())
            using(var reader = new StreamReader(stream)) {
                serverTextjson = await reader.ReadToEndAsync();
            }
            
            var scooterStations = JsonSerializer.Deserialize<ScooterStations>(serverTextjson,new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            
            if(scooterStations.Stations?.Find(station=>station.Name == stationName) == null) {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new NotFoundException($"Could not find the station: {stationName}");
            }
            return scooterStations.Stations.Where(station => station.Name == stationName).Select(station => station.BikesAvailable).FirstOrDefault();
        }
    }
}