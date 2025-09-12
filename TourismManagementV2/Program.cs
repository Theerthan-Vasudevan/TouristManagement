using Microsoft.EntityFrameworkCore;
using TourismManagementV2.DAL;
using TourismManagementV2.DAL.Interface;
using TourismManagementV2.DAL.Repository;

namespace TourismManagementV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            ////
            //// Enable static files (important!)
            //app.UseStaticFiles();

            // Add session services
            builder.Services.AddDistributedMemoryCache(); // stores session in memory
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDbContext<TourismDBContext>(options =>
            {
                string constr = builder.Configuration.GetConnectionString("constr");
                options.UseSqlServer(constr);
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
