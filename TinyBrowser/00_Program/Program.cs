using TinyBrowser._01_Browser;


class Program {
    
    static void Main(string[] args) {
        Browser browser = new Browser();
        
        browser.ClientConnect();
        browser.RequestAndReadWebsite();
        browser.FindTextBetweenTags();
        browser.StopReadWebsite();
    }
}


