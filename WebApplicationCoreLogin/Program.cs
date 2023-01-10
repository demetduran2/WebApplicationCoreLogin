using Microsoft.EntityFrameworkCore;
using WebApplicationCoreLogin.Models;

namespace WebApplicationCoreLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();//mvc yap�s� ve manage nuget packettekini bu projeye eklensin diye
            //builder.Services.AddDbContext<DatabaseContext>(o=>o.UseSqlServer("Server=203-BURAK\\NA;Database=MVCCore;Integrated Security=True"))
            builder.Services.AddDbContext<DatabaseContext>(o => 
            { 
                o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
                var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}