using System;
using System.IO;
using Din.ExternalModels.Utils;

namespace Din.Service.Classes
{
    public static class MainService
    {
        public static PropertyFile PropertyFile = new PropertyFile(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.UserProfile), "Din" + Path.DirectorySeparatorChar + "properties"));
    }
}
