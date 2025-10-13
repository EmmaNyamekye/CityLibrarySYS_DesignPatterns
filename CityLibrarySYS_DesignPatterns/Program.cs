using Microsoft.EntityFrameworkCore;
using CityLibrarySYS_DesignPatterns.Data; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register your DbContext
builder.Services.AddDbContext<LibraryDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CityLibraryDB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Your existing static assets setup
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
