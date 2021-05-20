using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHub_Explorer._01_Secrets;
using GitHub_Explorer._02_UserInfo;

namespace GitHub_Explorer._00_Program {
    class Program {
        static string separator = "******************************";
        static void Main(string[] args) {
            while (true) {
                var client = HttpClientSettings();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GitHub Explorer ready to use!");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Type a GitHub username to search for:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var userFirstInput = Console.ReadLine();

                var userTask = ReadFromGitHub(userFirstInput, client, false);
                userTask.Wait();
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Enter \"1\" to go to repositories");
                Console.WriteLine("Enter any other key to exit");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var userSecondInput = Console.ReadLine();
                if (userSecondInput == "1") {
                    var url = userFirstInput + "/repos";
                    var repoTask = ReadFromGitHub(url, client, true);
                    repoTask.Wait();
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Closing client.");
                client.Dispose();
            }
        }

        static async Task ReadFromGitHub(string userInput, HttpClient client, bool repos) {
            var response = await client.GetAsync(userInput);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            var streamReader = new StreamReader(stream);
            var responseData = await streamReader.ReadToEndAsync();

            if (repos) {
                Console.WriteLine(responseData);
                var userRepos = JsonSerializer.Deserialize<List<UserRepository>>(responseData);
                SeparateLines();
                foreach (var repo in userRepos) {
                    repo.DisplayRepoInfo();
                }
                SeparateLines();
            }
            else {
                var userPageInformation = JsonSerializer.Deserialize<UserPageInformation>(responseData);
                SeparateLines();
                userPageInformation.DisplayUserInfo();
                SeparateLines();
            }
        }

        static void SeparateLines() {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 3; i++)
                Console.WriteLine(separator);
            Console.ResetColor();
        }
        
        static HttpClient HttpClientSettings() {
            HttpClient client = new HttpClient();
            var token = Secrets.token; 
                
            client.BaseAddress = new Uri("https://api.github.com/users/");
            client.DefaultRequestHeaders.UserAgent.Add
                (new ProductInfoHeaderValue("GithubExplorer", "1.0"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("token", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
