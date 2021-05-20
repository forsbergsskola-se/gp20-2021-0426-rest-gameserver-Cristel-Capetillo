using System;

namespace GitHub_Explorer._02_UserInfo {
    public class UserPageInformation {
        string name { get; set; }
        string organisation { get; set; }
        string location { get; set; }
        string email { get; set; }
        string bio { get; set; }
        int publicRepositories { get; set; }
        int privateRepositories { get; set; }

        public void DisplayUserInfo() {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Organisation: " + organisation);
            Console.WriteLine("Bio: " + bio);
            Console.WriteLine("Location: " + location);
            Console.WriteLine("Public repositories: " + publicRepositories);
            Console.WriteLine("Private repositories: " + privateRepositories);
            Console.WriteLine("Email: " + email);
            Console.ResetColor();
        }
    }
}
