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
