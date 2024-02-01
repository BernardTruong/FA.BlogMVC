using FA.JustBlog.Core.Mail;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
//using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
   .AddEntityFrameworkStores<BlogDbContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddRazorPages();

// Configuration
var configuration = builder.Configuration;

builder.Services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

builder.Services.AddTransient<IEmailSender, SendMailService>();
//builder.Services.AddScoped<RoleManager<Role>>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/403";
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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapFallbackToPage("/Areas/Identity/Pages/Account/Register", "/account");

    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Posts}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "Post",
    pattern: "Post/{year:int}/{month:int}/{id}",
    defaults: new { controller = "Post", action = "Details" },
    constraints: new { year = @"\d{4}", month = @"\d{2}" }
);

    endpoints.MapControllerRoute(
    name: "Post",
    pattern: "Post/{Category?}",
    defaults: new { controller = "Post", action = "GetPostByCate" }
);
    endpoints.MapControllerRoute(
    name: "Tag",
    pattern: "Tag/{tag?}",
    defaults: new { controller = "Tag", action = "GetTagByCate" }
);
});

app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
