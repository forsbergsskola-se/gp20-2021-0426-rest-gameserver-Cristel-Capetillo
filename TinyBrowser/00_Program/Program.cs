using TinyBrowser._01_Browser;

namespace TinyBrowser._00_Program {
    class Program {
    
        static void Main(string[] args) {
            Browser browser = new Browser();
        
            browser.ClientConnect();
            browser.RequestAndReadWebsite();
            browser.FindTextBetweenTags();
            browser.ReadUserInput();
            browser.StopReadWebsite();
        }
    }
}


