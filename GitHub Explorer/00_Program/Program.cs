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
                var input = Console.ReadLine();
                client.BaseAddress = new Uri("https://api.github.com/users/");
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, input);
                client.Send(requestMessage);
                
                Console.WriteLine("Sending...");
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                var stream = responseMessage.Content.ReadAsStream();

                Console.WriteLine("Received response...");
                StreamReader streamReader = new StreamReader(stream);
                var stringFromStream = streamReader.ReadToEnd();
                Console.WriteLine(stringFromStream);
                Console.WriteLine("End");

                client.Dispose();
            }
        }
    }
}
