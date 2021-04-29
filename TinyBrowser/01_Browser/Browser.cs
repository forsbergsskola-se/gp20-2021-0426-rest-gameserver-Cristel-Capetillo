using System;
using System.IO;
using System.Net.Sockets;

namespace TinyBrowser._01_Browser {
    public class Browser {
        readonly TcpClient tcpClient = new TcpClient();
        string hostName = "www.acme.com";
        int port = 80;

        public void ClientConnect() {
            tcpClient.Connect(hostName, port);
        }
        
        
        public void ReadWebsite() {
            var networkStream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(networkStream);
            var streamReader = new StreamReader(networkStream);
            
            string requestedData = "";
            requestedData += "GET / HTTP/1.1\r\n";
            requestedData += "Host: www.acme.com\r\n\r\n";
           
            
            streamWriter.Write(requestedData);
            streamWriter.Flush();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Displaying website contents: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(streamReader.ReadToEnd());
        }


        public void StopReadWebsite() {
            tcpClient.Close();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Done reading website\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
