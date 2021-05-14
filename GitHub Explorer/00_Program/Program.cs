using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GitHub_Explorer._01_Secrets;
using GitHub_Explorer._02_User;

namespace GitHub_Explorer._00_Program {
    class Program {
        
        static string separator = "**********************************************************************************";
        static void Main(string[] args) {
            while (true) {
                HttpClient client = new HttpClient();
                var token = Secrets.Token;
                
                
                client.BaseAddress = new Uri("https://api.github.com/users/");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GithubExplorer", "1.0"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("GitHub Explorer ready! \nType a GitHub username:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var userInput = Console.ReadLine();

                var task = ReadFromGitHub(userInput, client);
                task.Wait();
                
                client.Dispose();
            }
        }

        static async Task ReadFromGitHub(string userInput, HttpClient client) {
            var response = await client.GetAsync(userInput);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            var streamReader = new StreamReader(stream);
            var responseData = await streamReader.ReadToEndAsync();

            var userResponse = JsonSerializer.Deserialize<UserResponse>(responseData);
           
            SeparateLines();
            Console.WriteLine(response);
            SeparateLines();
            Console.WriteLine(responseData);
            SeparateLines();
        }

        static void SeparateLines() {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 3; i++)
                Console.WriteLine(separator);
            Console.ResetColor();
        }
    }
}
