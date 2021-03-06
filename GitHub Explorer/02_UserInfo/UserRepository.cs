using System;

namespace GitHub_Explorer._02_UserInfo {
    public class UserRepository {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime pushedAt { get; set; }
        public DateTime LastPush => pushedAt.ToLocalTime();

        public void DisplayRepoInfo() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Description: " + description);
            Console.WriteLine("Last updated: " + LastPush);
            Console.ResetColor();
        }
    }
}