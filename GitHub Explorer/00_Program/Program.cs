using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHub_Explorer._01_Secrets;
using GitHub_Explorer._02_Request;

namespace GitHub_Explorer._00_Program {
     class Program {
        public static void Main(string[] args) {
            Menu();
        }

        static void Menu() {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Search for a Username");
            Console.WriteLine("2. Search for a Username and get all their repositories");

            int.TryParse(Console.ReadLine(), out var input);
            
            switch (input) {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Type in the username you want to search for");
                    Task.WaitAll(SearchForUser(Console.ReadLine()));
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Type in a username and get all their repositories");
                    Task.WaitAll(SearchForRepos(Console.ReadLine()));
                    break;
            }
        }
        
        public static async Task SearchForRepos(string userName)
        {
            Console.Clear();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            var token = ValidateSecrets.LoadToken();

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            var response = await client.GetAsync($"/users/{userName}/repos");
            
            StreamReader streamReader = new StreamReader(response.Content.ReadAsStream());
            
            var repoJson = await streamReader.ReadToEndAsync();
            streamReader.Close();
            client.Dispose();

            var reposDeserialize  = JsonSerializer.Deserialize<JsonElement>(repoJson);
            var repos = new ReadGitHubPage(reposDeserialize);

            for (int i = 0; i < repos.titles.Count; i++) {
                Console.WriteLine($"Name: {repos.titles[i]}");
                Console.WriteLine($"Description: {repos.descriptions[i]}");
                Console.WriteLine($"Url: {repos.hyperlinks[i]}");
                Console.WriteLine();
            }
            
            Console.WriteLine("Any. Go back to the main menu");
            Console.WriteLine("2. Go to user profile");
            var input = Console.ReadLine();
            Console.Clear();
            if (input == "2") {
                Task.WaitAll(SearchForUser(userName));
            }
            else {
                Menu();
            }
        }
        
        public static async Task SearchForUser(string userName)
        {
            Console.Clear();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            var token = ValidateSecrets.LoadToken();

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            
            var response = await client.GetAsync($"/users/{userName}");
            
            StreamReader sr = new StreamReader(response.Content.ReadAsStream());
            
            var usrJson = await sr.ReadToEndAsync();
            sr.Close();
            client.Dispose();
            
            var user = JsonSerializer.Deserialize<ReadGitHubUser>(usrJson);

            if (user != null)
                foreach (var info in user.info) {
                    Console.WriteLine(info);
                }

          
            Console.WriteLine("Any. Go back to the main menu");
            Console.WriteLine("2. Go to users repositories");
            var input = Console.ReadLine();
            Console.Clear();
            if (input == "2") {
                Task.WaitAll(SearchForRepos(userName));
            }
            else {
                Menu();
            }
        }
    }
}
