﻿using System;
using System.IO;
using System.Net.Sockets;

namespace TinyBrowser._01_Browser {
    public class Browser {
        TcpClient tcpClient = new();
        string host = "www.acme.com";
        string uri = "/";
        int port = 80;
        

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
            
            var linkTag = "<a href=\"";
            var linkIndexStarts = response.IndexOf(linkTag);
            string link = string.Empty;
            if (linkIndexStarts != -1) {
                linkIndexStarts += linkTag.Length;
                var linkIndexEnds = response.IndexOf("</a>");
                if (linkIndexEnds > linkIndexStarts) {
                   link = response[linkIndexStarts..linkIndexEnds];
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Website title: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Website links: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(link);
        }


        public void StopReadWebsite() {
            tcpClient.Close();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Done reading website\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
