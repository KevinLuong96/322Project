using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using _322Api.Models;

namespace _322Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DatabaseContext>();
                    if (context.Users.Count() == 0)
                    {
                        context.Users.Add(new User
                        {
                            Username = "test@test.com",
                            Password = "72BDqJZX4jSa9dkt/Yk8KcA0Ng1YRfFnbJa8cGcTixbWysaR",
                            Role = Roles.Admin,
                        });
                    }
                    if (context.ReviewSources.Count() == 0)
                    {
                        context.ReviewSources.Add(new ReviewSource
                        {
                            SourceName = "TheVerge",
                        });
                        context.ReviewSources.Add(new ReviewSource
                        {
                            SourceName = "TechRadar",
                        });
                        context.ReviewSources.Add(new ReviewSource
                        {
                            SourceName = "Cnet",
                        });
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
