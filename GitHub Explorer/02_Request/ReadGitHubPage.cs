using System.Collections.Generic;
using System.Text.Json;

namespace GitHub_Explorer._02_Request {
    
    public class ReadGitHubPage {
        public List<string> titles;
        public List<string> hyperlinks;
        public List<string> descriptions;

        public ReadGitHubPage(JsonElement deserializedJson) {
            titles = new List<string>();
            hyperlinks = new List<string>();
            descriptions = new List<string>();

            foreach (var element in deserializedJson.EnumerateArray()) {
                titles.Add(element.GetProperty("title").GetString());
                hyperlinks.Add(element.GetProperty("hyperlink").GetString());
                descriptions.Add(element.GetProperty("description").GetString());
            }
        }

    }
}
