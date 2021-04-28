using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace TinyBrowser._01_Browser {
    public class Browser {
        TcpClient tcpClient = new TcpClient();
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
            
            
            string FilteringStrings(string sourceStrings, char characterDivider) {
                var resultingText = requestedData;
                var shouldBeDisplayed = false;

                foreach (var character in sourceStrings) {
                    if (character == characterDivider) {
                        shouldBeDisplayed = !shouldBeDisplayed;
                    }

                    if (shouldBeDisplayed && character != '"') {
                        resultingText += character;
                    }
                }
                return resultingText;
            }
            
            
            var hyperlinks = Regex.Matches(requestedData, @"<(a|link).*?href=(""|')(.+?)(""|').*?>").Select(matching => matching.Value).ToArray();
            var linksHeaders = Regex.Matches(requestedData, @"\"">(.*?)\</a>").Select(matching => matching.Value).ToArray();
            

            foreach (var linkHeader in linksHeaders) {
                Console.WriteLine(linkHeader);
            }

            
            foreach (var hyperlink in hyperlinks) {
                Console.WriteLine(FilteringStrings(hyperlink,'"'));
            }
            
            
            streamWriter.Write(requestedData);
            streamWriter.Flush();

            Console.WriteLine("Reading website...");
            Console.WriteLine(streamReader.ReadToEnd());
        }


        public void StopReadingWebsite() {
            tcpClient.Close();
            Console.WriteLine("Done reading website\nPress any key to exit");
            Console.ReadKey();
        }

    }
}
