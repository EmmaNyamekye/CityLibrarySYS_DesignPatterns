using Microsoft.EntityFrameworkCore;
using CityLibrarySYS_DesignPatterns.Data;
using CityLibrarySYS_DesignPatterns.Data.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryDatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberService, MemberService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();