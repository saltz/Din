using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Din.ExternalModels.Utils;

namespace Din.Service
{
    public static class MainService
    {
        public static PropertyFile PropertyFile = new PropertyFile(Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.UserProfile), "Din" + Path.DirectorySeparatorChar + "properties"));
    }
}
