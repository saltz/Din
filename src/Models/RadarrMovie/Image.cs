using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RadarrMovie
{
    public class Image
    {
        public string covertype { get; set; }
        public string url { get; set; }

        public Image() { }

        public Image(string url)
        {
            this.covertype = "poster";
            this.url = url;
        }
    }




}
