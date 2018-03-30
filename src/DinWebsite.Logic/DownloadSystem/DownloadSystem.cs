using System;
using System.Collections.Generic;
using System.Text;
using DinWebsite.ExternalModels.DownloadClient;

namespace DinWebsite.Logic.DownloadSystem
{
    public class DownloadSystem
    {
        private readonly DownloadClient _downloadClient;

        public DownloadSystem()
        {
           _downloadClient = new DownloadClient();
        }

    }
}
