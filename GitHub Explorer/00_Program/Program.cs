using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using GitHub_Explorer._01_Secrets;

namespace GitHub_Explorer._00_Program {
    class Program {
        static void Main(string[] args) {
            var token = Secrets.Token;
            
            while (true) {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", token);
                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Type a GitHub username:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var userInput = Console.ReadLine();
                client.BaseAddress = new Uri("https://api.github.com/users/");
                
                HttpRequestMessage requestingGitHubData = new HttpRequestMessage(HttpMethod.Get, userInput);
                client.Send(requestingGitHubData);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Requesting website...");
                
                HttpResponseMessage receivingGitHubData = new HttpResponseMessage();
                var stream = receivingGitHubData.Content.ReadAsStream();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Receiving data from website...");
                
                StreamReader streamReader = new StreamReader(stream);
                var dataReceived = streamReader.ReadToEnd();
                Console.WriteLine(dataReceived);
                Console.WriteLine("Finished");
                
                client.Dispose();
            }
        }
    }
}
