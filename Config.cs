using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PvPChecks {
    public class Config {
        public string[] WeaponList = new string[]
        {
            "Last Prism",
            "Meowmere",
            "Nebula Blaze",
            "S.D.M.G.",
            "Star Wrath",
            "Terrarian",
            "Lunar Flare",
            "Coin Gun"
        };

        public int[] BuffList = new int[] { };

        public void Write(string path) {
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public static Config Read(string path) {
            if (!File.Exists(path))
                return new Config();
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }
    }
}
