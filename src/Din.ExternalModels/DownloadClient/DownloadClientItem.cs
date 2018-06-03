using System.Collections.Generic;
using Newtonsoft.Json;

namespace Din.ExternalModels.DownloadClient
{
    public class DownloadClientItem
    {
        [JsonProperty("comment")] public string Comment { get; set; }
        [JsonProperty("hash")] public string Hash { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("save_path")] public string SavePath { get; set; }
        [JsonProperty("eta")] public int Eta { get; set; }
        [JsonProperty("files")] public List<DownloadClientFile> Files { get; set; }
        [JsonProperty("file_progress")] public List<double> FileProgress { get; set; }
    }

    public class DownloadClientFile
    {
        [JsonProperty("index")] public double Index { get; set; }
        [JsonProperty("path")] public string Path { get; set; }
        [JsonProperty("offset")] public double Offset { get; set; }
        [JsonProperty("size")] public double Size { get; set; }
    }
}