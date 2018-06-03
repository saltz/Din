using System;
using System.IO;
using Din.ExternalModels.Utils;

namespace Din.Service.Classes
{
    /// <summary>
    ///    MainService that provides the necessary properties.
    /// </summary>
    public static class MainService
    {
        //Debug Location
        public static readonly PropertyFile PropertyFile = new PropertyFile(@"PropertyFile");

        //Release Location
        //public static readonly PropertyFile PropertyFile = new PropertyFile("/propdir/PropertyFile");
    }
}
