using System;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser {
    
    class Program {
        static void Main(string[] args) {
            var tcpClient = new TcpClient("acme.com", 80);
            var stream = tcpClient.GetStream();
            var bytes = Encoding.ASCII.GetBytes("GET / HTTP/1.1\r\nHost: acme.com\r\n\r\n");
            stream.Write(bytes);
        }
    }
}
