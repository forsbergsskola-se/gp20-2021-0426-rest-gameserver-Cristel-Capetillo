using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using TinyBrowser._02_LinksAndTitles;

namespace TinyBrowser._01_Browser {
    public class Browser {
        TcpClient tcpClient = new();
        string host = "www.acme.com";
        string uri = "/";
        int port = 80;
        static AllLinksAndTitles[] links;

        public void ClientConnect() {
            tcpClient.Connect(host, port);
        }
        
        
        public void ReadWebsite() {
            var networkStream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(networkStream);
            

            string requestedData = "";
            requestedData += "GET / HTTP/1.1\r\n";
            requestedData += "Host: www.acme.com\r\n\r\n";
           
            
            streamWriter.Write(requestedData);
            streamWriter.Flush();


            var uriBuilder = new UriBuilder(null, host);
            uriBuilder.Path = uri;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Displaying opened website: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(uriBuilder);
        }
        

        public void FindTextBetweenTags() {
            var networkStream = tcpClient.GetStream();
            var streamReader = new StreamReader(networkStream);
            var response = streamReader.ReadToEnd();

            DisplayWebsitesTitle(response);
            links = FilterAllLinksWithTitles(response).ToArray();
            DisplayWebsitesLinks();
        }

        
        void DisplayWebsitesTitle(string response) {
            var titleTag = "<title>";
            var titleIndexStarts = response.IndexOf(titleTag);
            string title = string.Empty;
            
            if (titleIndexStarts != -1) {
                titleIndexStarts += titleTag.Length;
                var titleIndexEnds = response.IndexOf("</title>");
                if (titleIndexEnds > titleIndexStarts) {
                    title = response[titleIndexStarts..titleIndexEnds];
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Website's title: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
        }
        
        
        void DisplayWebsitesLinks(){
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Displaying all links on the website: ");
            if (links != null){
                for (var i = 0; i < links.Length; i++){
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{i}: {links[i].displayLinksText} ({links[i].links})");
                }   
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No links found");
            }
        }
        
        
        public void StopReadWebsite() {
            tcpClient.Close();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Website's content displayed\nPress any key to exit");
            Console.ReadKey();
        }
        
        
        static IEnumerable<AllLinksAndTitles> FilterAllLinksWithTitles(string response) {
            var linkTag = "<a href=\"";
            var quotationMark = '"';
            var linkIndexStarts = '>';
            var linkIndexEnds = "</a>";
            
            var allLinksList = new List<AllLinksAndTitles>();
            var arrayFilter = response.Split(linkTag);
            arrayFilter = arrayFilter.Skip(1).ToArray();
            
            foreach (var dataFiltered in arrayFilter){
                var hyperlink = dataFiltered.TakeWhile(symbol => symbol != quotationMark).ToArray();
                var filterAfterHyperlink = dataFiltered[hyperlink.Length..];
                var filteredDataStartsAt = filterAfterHyperlink.IndexOf(linkIndexStarts) + 1;
                var filteredDataEndsAt = filterAfterHyperlink.IndexOf(linkIndexEnds, StringComparison.Ordinal);
                var dataToDisplay = 
                    filterAfterHyperlink.Substring(filteredDataStartsAt,(filteredDataEndsAt - filteredDataStartsAt))
                    .Replace("<b>", string.Empty).Replace("</b>", string.Empty);
                if (dataToDisplay.StartsWith("<img")){
                    dataToDisplay = "Image";
                }
                allLinksList.Add(new AllLinksAndTitles{
                    links = new string(hyperlink),
                    displayLinksText = new string(dataToDisplay)
                });
            }
            return allLinksList;
        }
    }
}
