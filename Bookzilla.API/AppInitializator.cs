using Bookzilla.API.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace Bookzilla.API
{
    public class AppInitializator
    {
        public void CreateDbIfNotExists(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BookzillaDbContext>();
                    //DbInitializer.Initialize(context);
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
