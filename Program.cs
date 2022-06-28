using Projekt.NET.DAL;
using Microsoft.EntityFrameworkCore;
using Projekt.NET.Data;
using Projekt.NET.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin");
});


builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", config =>
    {
        config.Cookie.HttpOnly = true;
        config.Cookie.SecurePolicy = CookieSecurePolicy.None;
        config.Cookie.Name = "UserLoginCookie";
        config.LoginPath = "/Login/UserLogin";
        config.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddDbContext<ProjektNETContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjektNETContext") ?? throw new InvalidOperationException("Connection string 'ProjektNETContext' not found.")));

builder.Services.AddTransient<IDatabase, SqlDatabase>();
//builder.Services.AddTransient<IDatabase, JsonDatabase>();



var app = builder.Build();

// Database structure
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ProjektNETContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Middleware
app.UseImageMiddleware();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
