using System;
using System.IO;
using System.Net.Sockets;
using TinyBrowser._01_Browser;


class Program {
    
    static void Main(string[] arguments) {
        Browser browser = new Browser();
        
        browser.ClientConnect();
        browser.ReadWebsite();
        browser.FilteringStrings(" ", '"');
        browser.StopReadingWebsite();
    }
}

