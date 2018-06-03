using System.Collections.Generic;
using System.Linq;

namespace Din.ExternalModels.Utils
{
    public class PropertyFile
    {
        private Dictionary<string, string> _list;

        public PropertyFile(string file)
        {
            Reload(file);
        }

        public string Get(string field)
        {
            return (_list.ContainsKey(field)) ? (_list[field]) : (null);
        }

        private void Reload(string filename)
        {
            _list = new Dictionary<string, string>();
            if (System.IO.File.Exists(filename))
                LoadFromFile(filename);
            else
                System.IO.File.Create(filename);
        }

        private void LoadFromFile(string file)
        {
            foreach (string line in System.IO.File.ReadAllLines(file))
            {
                if ((!string.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    int index = line.IndexOf('=');
                    string key = line.Substring(0, index).Trim();
                    string value = line.Substring(index + 1).Trim();

                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    try
                    {
                        //ignore dublicates
                        _list.Add(key, value);
                    }
                    catch { }
                }
            }
        }
    }
}
