
using Microsoft.EntityFrameworkCore;

namespace Din.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DinContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
