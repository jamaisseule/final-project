using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Final.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LumosTutorIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LumosTutorIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'LumosTutorIdentityDbContextConnection' not found.")));

builder.Services.AddIdentity<LumosTutorUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<LumosTutorIdentityDbContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MovieTicket.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<LumosTutorUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await ContextSeed.SeedRolesAsync(userManager, roleManager);
    await ContextSeed.SeedAdminAsync(userManager, roleManager);
}

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=TutorHome}/{id?}");

app.MapRazorPages();
app.Run();








// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.AspNetCore.Identity;
// using Final.Areas.Identity.Data;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.EntityFrameworkCore;



// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<LumosTutorIdentityDbContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("LumosTutorIdentityDbContextConnection")
//         ?? throw new InvalidOperationException("Connection string 'LumosTutorIdentityDbContextConnection' not found."));
// });

// builder.Services.AddIdentity<LumosTutorUser, IdentityRole>(options =>
// {
//     options.SignIn.RequireConfirmedAccount = true;
// })
// .AddEntityFrameworkStores<LumosTutorIdentityDbContext>();

// var app = builder.Build();

// using (var scope = app.Services.CreateScope()){
//     var services = scope.ServiceProvider;
//     var userManager = services.GetRequiredService<UserManager<LumosTutorUser>>();
//     var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//     await ContextSeed.SeedRolesAsync(userManager, roleManager);
//     await ContextSeed.SeedAdminAsync(userManager, roleManager);
// }

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();
// app.UseSession();
// app.UseAuthentication();;

// app.UseAuthorization();
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=}/{action=}/{id}");
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=TutorHome}/");




// app.MapRazorPages();
// app.Run();









// // var builder = WebApplication.CreateBuilder(args);
// // var app = builder.Build();

// // app.MapGet("/", () => "Hello World!");


// // app.Run();
