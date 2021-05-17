using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHub_Explorer._01_Secrets;

namespace GitHub_Explorer._00_Program {
    
    public class UserResponse {
        public string name { get; set; }
        public string company { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public string bio { get; set; }
        public int public_repos { get; set; }
        public int private_Repos { get; set; }

        public void DisplayUserInfo() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Company: " + company);
            Console.WriteLine("Bio: " + bio);
            Console.WriteLine("Location: " + location);
            Console.WriteLine("Private repos: " + private_Repos);
            Console.WriteLine("public repos: " + public_repos);
            Console.WriteLine("Email: " + email);
            Console.ResetColor();
        }
    }

    public class UserRepos {
        
    }
    
    class Program {
        static string separator = "******************************";
        static void Main(string[] args) {
            while (true) {
                var client = HttpClientSettings();
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("GitHub Explorer ready! \nType a GitHub username:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var userInput = Console.ReadLine();

                var userTask = ReadFromGitHub(userInput, client, false);
                userTask.Wait();
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Enter \"1\" to go to repositories");
                Console.WriteLine("Enter any other input to exit");
                Console.ForegroundColor = ConsoleColor.Yellow;
                userInput = Console.ReadLine();
                if (userInput == "1") {
                    var url = userInput + "/repos";
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

            UserResponse userResponse;
            if (repos) {
               userResponse = JsonSerializer.Deserialize<UserResponse>(responseData); 
            }
            else {
               userResponse = JsonSerializer.Deserialize<UserResponse>(responseData);
            }
           
            SeparateLines();
            if (userResponse != null)
                userResponse.DisplayUserInfo();
            SeparateLines();
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
