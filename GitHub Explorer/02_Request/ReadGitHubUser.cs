using System.Collections;
using System.Collections.Generic;

namespace GitHub_Explorer._02_Request {
    
    public class ReadGitHubUser {
        public string username { get; set; }
        public string organisation { get; set; }
        public string blog { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public int publicRepos { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }

        public UserInfo info => new UserInfo(this);
    }

    public class UserInfo : ReadGitHubUser, IEnumerable, IEnumerator {
        int current = -1;
        List<string> information = new List<string>();
        ReadGitHubUser response;
        public UserInfo(ReadGitHubUser Response) {
            response = Response;
        }

        public IEnumerator GetEnumerator() {
            if (!string.IsNullOrEmpty(response.username)) {
                information.Add($"Username: {response.username}");
            }
            else {
                information.Add("Username: Not found ");
            }

            if (!string.IsNullOrEmpty(response.organisation)) {
                information.Add($"Organisation: {response.organisation}");
            }
            else {
                information.Add("Organisation: Not found");
            }
            
            if (!string.IsNullOrEmpty(response.blog)) {
                information.Add($"Blog: {response.blog}");
            }
            else {
                information.Add("Blog: Not found ");
            }
            
            if (!string.IsNullOrEmpty(response.location)) {
                information.Add($"Location: {response.location}");
            }
            else {
                information.Add("Location: Not found ");
            }
            
            if (!string.IsNullOrEmpty(response.email)) {
                information.Add($"Email: {response.email}");
            }
            else {
                information.Add("Email: Not found ");
            }
            
            if (!string.IsNullOrEmpty(response.publicRepos.ToString())) {
                information.Add($"Public repositories: {response.publicRepos}");
            }
            else {
                information.Add("Public repositories: Not found ");
            }
            
            if (!string.IsNullOrEmpty(response.followers.ToString())) {
                information.Add($"Followers: {response.followers}");
            }
            else {
                information.Add("Followers: 0 ");
            }
            
            if (!string.IsNullOrEmpty(response.following.ToString())) {
                information.Add($"Following: {response.following}");
            }
            else {
                information.Add("Following: 0 ");
            }
            
            if (!string.IsNullOrEmpty(response.createdAt)) {
                information.Add($"Created: {response.createdAt}");
            }
            else {
                information.Add("Created: Not found ");
            }
            
            if (!string.IsNullOrEmpty(response.updatedAt)) {
                information.Add($"Updated: {response.updatedAt}");
            }
            else {
                information.Add("Updated: Not found ");
            }

            return this;
        }

        public bool MoveNext() {
            current++;
            return current < information.Count;
        }

        public void Reset() {
            current = 0;
        }

        public object Current => information[current];
    }
}
