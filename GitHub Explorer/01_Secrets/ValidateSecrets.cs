using System;
using System.IO;
using System.Text.Json;

namespace GitHub_Explorer._01_Secrets {
    
    public static class ValidateSecrets {

        public static string LoadToken() {
            Secrets secrets;
            if (!File.Exists("secrets.json")) {
                secrets = new Secrets();
                File.WriteAllText("secrets.json", JsonSerializer.Serialize(secrets));
            }
            else {
                secrets = JsonSerializer.Deserialize<Secrets>(File.ReadAllText("secrets.json"));
            }
            if (string.IsNullOrEmpty(secrets.Token)) {
                throw new Exception("Please define a token in secrets.json file");
            }
            return secrets.Token;
        }
    }
}
