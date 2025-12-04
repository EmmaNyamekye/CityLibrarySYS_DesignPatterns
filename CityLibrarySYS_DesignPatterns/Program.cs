using Microsoft.EntityFrameworkCore;
using CityLibrarySYS_DesignPatterns.Data;
using CityLibrarySYS_DesignPatterns.Data.Services;
using Microsoft.AspNetCore.Builder;
using CityLibrarySYS_DesignPatterns.Data.Services.Commands; // Required for LoanCommandInvoker
using CityLibrarySYS_DesignPatterns.Data.Services.Observers; // <<< FIX: THIS LINE WAS MISSING IN PREVIOUS VERSIONS >>>
using System;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryDatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// --- Caching/Decorator Setup ---
builder.Services.AddMemoryCache();

// --- SERVICE REGISTRATION ---

// Decorator Chain for IBookService
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<IBookService, CachingBookService>();

// Standard Scoped Services
builder.Services.AddScoped<IMemberService, MemberService>();

// Singleton Service (for Loan Rules)
builder.Services.AddSingleton<ILoanRules, LoanRules>();

// Loan Service (The Receiver for Command, The Subject/Publisher for Observer)
builder.Services.AddScoped<ILoanService, LoanService>();

// --- COMMAND PATTERN REGISTRATION ---
// The Invoker is registered as Scoped to manage request-specific commands.
builder.Services.AddScoped<LoanCommandInvoker>();

// --- OBSERVER PATTERN REGISTRATION ---
// The Observer is registered as Singleton so it subscribes once and listens
// for the entire application lifetime, ensuring all events are caught.
builder.Services.AddSingleton<MemberStatusObserver>();


// Add Session services, essential for managing the loan cart in the controller
builder.Services.AddSession(options =>
{
    // Set a moderate timeout for the loan session
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ensure static files middleware is present if not already

// --- MIDDLEWARE CONFIGURATION ---
// IMPORTANT: UseSession must be called before UseRouting/MapControllerRoute
app.UseSession();

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Home}/{action=Index}/{id?}")
 .WithStaticAssets();

app.Run();