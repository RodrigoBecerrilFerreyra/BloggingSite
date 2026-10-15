using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlogSite.Models;

namespace BlogSite.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        // get services needed to seed data
        var context     = services.GetRequiredService<BlogDbContext>();
        var usermanager = services.GetRequiredService<UserManager<User>>();
        var config      = services.GetRequiredService<IConfiguration>();

        await context.Database.MigrateAsync();

        // if there are no categories in the database then add the blogpost category
        if (!await context.Categories.AnyAsync())
        {
            context.Categories.Add(new Category{Name = "blogpost"});
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            // add the admin user to the database
            // reading the email and password will fail if not configured correctly
            string? email = config["Admin:Email"];
            string? password = config["Admin:Password"];
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException("Admin email or password are not set.");
            }

            var result = await usermanager.CreateAsync(new User{UserName = email, Email = email}, password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Could not create admin user: {errors}");
            }
        }
    }
}
