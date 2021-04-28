using System;
using System.IO;
using System.Net.Sockets;


public static class Program {
    static void Main(string[] arguments) {
        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect("www.acme.com", 80);

        using (NetworkStream networkStream = tcpClient.GetStream()) {
            StreamWriter streamWriter = new StreamWriter(networkStream);
            StreamReader streamReader = new StreamReader(networkStream);

            string requestHttp = "";
            requestHttp += "GET / HTTP/1.1\r\n";
            requestHttp += "Host: www.acme.com\r\n\r\n";
            
            streamWriter.Write(requestHttp);
            streamWriter.Flush();

            Console.WriteLine("Reading website...");
            Console.WriteLine(streamReader.ReadToEnd());
        }

        tcpClient.Close();
        Console.WriteLine("Done reading website\nPress any key to exit");
        Console.ReadKey();
    }
}

