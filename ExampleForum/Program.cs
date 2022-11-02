using Microsoft.EntityFrameworkCore;
using ExampleForum.Data;
using Microsoft.AspNetCore.Identity;
using ExampleForum.Areas.Identity.Data;
using ExampleForum.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ExampleForumContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExampleForumContext") ?? throw new InvalidOperationException("Connection string 'ExampleForumContext' not found.")));

builder.Services.AddDefaultIdentity<ExampleForumUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExampleForumContext>();

builder.Services.AddRazorPages().AddRazorPagesOptions(o =>
{
    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowCredentials();
                      });
});

builder.Services.AddSpaStaticFiles(config =>
{
    config.RootPath = "frontend";
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.AccessDeniedPath = null;
    options.SlidingExpiration = true;
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

builder.Services.AddScoped<BoardService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<ThreadService>();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition
                      = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();
app.UseCors("CORS");
app.Use(async (ctx, next) => // 404 handler
{
    await next();

    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        string? originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Errors/404";
        await next();
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var webRootProvider =
  new PhysicalFileProvider(builder.Environment.WebRootPath);

var spaPathProvider =
  new PhysicalFileProvider(
   Path.Combine(Environment.CurrentDirectory, @"frontend-dist/"));

var compositeProvider =
  new CompositeFileProvider(webRootProvider,
                            spaPathProvider);

app.Environment.WebRootFileProvider = compositeProvider;

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
