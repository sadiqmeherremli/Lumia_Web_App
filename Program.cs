using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lumia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddDbContext<AppDbContext>( opt=>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
          
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}