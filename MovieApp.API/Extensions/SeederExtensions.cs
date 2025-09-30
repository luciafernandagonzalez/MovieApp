using Microsoft.EntityFrameworkCore;
using MovieApp.Domain;
using MovieApp.Infrastructure.Data;

namespace MovieApp.API.Extensions
{
    public static class SeederExtensions
    {
        public static async Task SeedDataAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MovieAppDbContext>();

                await context.Database.MigrateAsync();

                if (!await context.Users.AnyAsync()) 
                {
                    var adminUser = new User
                    {
                        Username = "admin",
                        Password = BCrypt.Net.BCrypt.HashPassword("adminpass"),
                        Role = "Admin",
                    };

                    await context.Users.AddAsync(adminUser);
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
