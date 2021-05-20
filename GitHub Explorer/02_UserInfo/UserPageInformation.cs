using System;

namespace GitHub_Explorer._02_UserInfo {
    public abstract class UserPageInformation {
        public string name { get; set; }
        public string organisation { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public string bio { get; set; }
        public int publicRepositories { get; set; }
        public int privateRepositories { get; set; }

        public void DisplayUserInfo() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Name: " + ConsoleColor.Yellow + name);
            Console.WriteLine("Organisation: " + ConsoleColor.Yellow + organisation);
            Console.WriteLine("Bio: " + ConsoleColor.Yellow + bio);
            Console.WriteLine("Location: " + ConsoleColor.Yellow + location);
            Console.WriteLine("Public repositories: " + ConsoleColor.Yellow + publicRepositories);
            Console.WriteLine("Private repositories: " + ConsoleColor.Yellow + privateRepositories);
            Console.WriteLine("Email: " + ConsoleColor.Yellow + email);
            Console.ResetColor();
        }
    }
}
