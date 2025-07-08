using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Ozon.Db;
using Ozon.Db.Models;
using Serilog;

namespace OnlineShopWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", "Online Shop");
            });

            // Add services to the container.
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connection);
                options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            });
            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(connection);
                options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<IdentityContext>();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(12);
                options.LoginPath = "/Login/index";
                options.LogoutPath = "/Login/index";
                options.Cookie = new CookieBuilder()
                {
                    IsEssential = true
                };
            });
            builder.Services.AddSingleton<IImagesService, ImagesService>();
            builder.Services.AddTransient<IRolesStorage, InDbRolesStorage>();
            builder.Services.AddTransient<ILikeStorage, InDbLikeStorage>();
            builder.Services.AddTransient<ICompareStorage, InDbCompareStorage>();
            builder.Services.AddTransient<IOrdersStorage, InDbOrdersStorage>();
            builder.Services.AddTransient<ICartsStorage, InDbCartsStorage>();
            builder.Services.AddTransient<IProductsStorage, InDbProductsStorage>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                IdentityInitializer.Initialize(userManager, roleManager);
            }

            app.UseRequestLocalization("en-UY", "fr-FR");
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "../Ozon.Db/ArchiveImages")),

                RequestPath = new PathString("/ImagesDb")

            });


            app.MapControllerRoute(
                name: "MyAreas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}