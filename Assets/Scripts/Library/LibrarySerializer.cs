using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assets.Scripts.Library
{
    public class LibrarySerializer
    {
        public const string BLUEPRINT_EXTENSION = ".ship.png";
        public const string BLUEPRINT_FILENAME_PATTERN = "*" + BLUEPRINT_EXTENSION;

        private string _file;
        private string _shipFolder;
        private IRankMatcher _rankMatcher;

        public LibrarySerializer(string file, string shipFolder, IRankMatcher matcher)
        {
            _file = file;
            _shipFolder = shipFolder;
            _rankMatcher = matcher;
        }

        public Library Load()
        {
            var library = new Library(_rankMatcher);

            var files = Directory.GetFiles(_shipFolder, BLUEPRINT_FILENAME_PATTERN);
            var savedBlueprints = ReadSave(_file);

            foreach (var file in files)
            {
                var name = GetBlueprintName(file);
                var saved = savedBlueprints.Find(b => b.Name.Equals(name));

                var blueprint = saved == null ? new Blueprint() : new Blueprint(saved);
                blueprint.Name = name;
                blueprint.IconPath = file;

                library.Add(blueprint);
            }

            return library;
        }

        public void Save(Library library)
        {
            WriteSave(_file, library);
        }

        private string GetBlueprintName(string file)
        {
            return Path.GetFileName(file).Replace(BLUEPRINT_EXTENSION, "");
        }

        private void WriteSave(string file, Library library)
        {
            string json = JsonConvert.SerializeObject(library.ToList(), Formatting.Indented);
            File.WriteAllText(file, json);
        }

        private List<Blueprint> ReadSave(string file)
        {
            if (!File.Exists(file)) return new List<Blueprint>();

            string json = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<List<Blueprint>>(json);
        }
    }
}
