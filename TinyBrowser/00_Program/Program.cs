using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

public static class Program {
    static void Main(string[] arguments) {
        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect("www.acme.com", 80);

        using (NetworkStream networkStream = tcpClient.GetStream()) {
            StreamWriter streamWriter = new StreamWriter(networkStream);
            StreamReader streamReader = new StreamReader(networkStream);

            string requestHttp = "";
            requestHttp += "GET / HTTP/1.0\r\n";
            requestHttp += "Host: www.acme.com\r\n";
            requestHttp += "\r\n";

            streamWriter.Write(requestHttp);
            streamWriter.Flush();

            Console.WriteLine("[Reading website...]");
            Console.WriteLine(streamReader.ReadToEnd());
        }

        tcpClient.Close();
        Console.WriteLine("[Done reading website!]");
        Console.ReadKey();
    }
}

