using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using PermissionBasedAuthorization.Data;
using PermissionBasedAuthorization.Permission;
using PermissionBasedAuthorization.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI()
.AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // For non-area controllers: /Pages/{Controller}/{View}.cshtml and /Pages/Shared/{View}.cshtml
    options.ViewLocationFormats.Insert(0, "/Pages/{1}/{0}.cshtml");
    options.ViewLocationFormats.Insert(0, "/Pages/Shared/{0}.cshtml");

    // For area controllers with Pages under Areas/{Area}/Pages/...
    options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Pages/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Pages/Shared/{0}.cshtml");
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("app");
try
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DefaultRoles.SeedAsync(userManager, roleManager);
    await DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
    await DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
    logger.LogInformation("Finished Seeding Default Data");
    logger.LogInformation("Application Starting");
}
catch (Exception ex)
{
    logger.LogWarning(ex, "An error occurred seeding the DB");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

//app.MapStaticAssets();
//app.MapRazorPages()
//   .WithStaticAssets();

app.Run();
