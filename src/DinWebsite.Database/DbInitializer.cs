using System;
using System.Collections.Generic;
using System.Text;

namespace DinWebsite.Database
{
    public static class DbInitializer
    {
        public static void Initialize(DinWebsiteContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
