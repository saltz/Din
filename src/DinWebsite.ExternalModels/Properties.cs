using System;
using System.Collections.Generic;
using System.Linq;

namespace DinWebsite.Logic
{
    public class Properties
    {
        private Dictionary<string, string> _list;
        private string _filename;

        public Properties(string file)
        {
            Reload(file);
        }

        public string Get(string field, string defValue)
        {
            return (Get(field) == null) ? (defValue) : (Get(field));
        }

        public string Get(string field)
        {
            return (_list.ContainsKey(field)) ? (_list[field]) : (null);
        }

        public void Set(string field, Object value)
        {
            if (!_list.ContainsKey(field))
                _list.Add(field, value.ToString());
            else
                _list[field] = value.ToString();
        }

        public void Save()
        {
            Save(_filename);
        }

        public void Save(string filename)
        {
            _filename = filename;

            if (!System.IO.File.Exists(filename))
                System.IO.File.Create(filename);

            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);

            foreach (string prop in _list.Keys.ToArray())
                if (!string.IsNullOrWhiteSpace(_list[prop]))
                    file.WriteLine(prop + "=" + _list[prop]);

            file.Close();
        }

        public void Reload()
        {
            Reload(_filename);
        }

        public void Reload(string filename)
        {
            _filename = filename;
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
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

    }
}
