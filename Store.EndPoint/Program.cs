using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Store.Application;
using Store.Application.Interfaces.Context;
using Store.Application.Interfaces.FacadePatterns;
using Store.Application.Services.Common.Queries.GetMenuCategories;
using Store.Application.Services.Products.Facade;
using Store.Application.Services.Users.FacadePattern;
using Store.Common.Roles;
using Store.EndPoint.Tools;
using Store.Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// My Services
var connectionstring = builder.Configuration.GetSection("ConnectionString").Value;
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(connectionstring));
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

builder.Services.AddApplicationServices();

builder.Services.AddScoped<IUserFacade, UserFacade>();

builder.Services.AddScoped<IProductFacade, ProductFacade>();

builder.Services.AddScoped<IGetMenuCategoriesService, GetMenuCategoriesService>();

builder.Services.AddScoped<Store.EndPoint.Tools.ICookieManager, CookieManager>();





builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(Enum.GetName(typeof(BaseRoles), BaseRoles.Admin), policy => policy.RequireRole(Enum.GetName(typeof(BaseRoles), BaseRoles.Admin)));
    option.AddPolicy(Enum.GetName(typeof(BaseRoles), BaseRoles.Operator), policy => policy.RequireRole(Enum.GetName(typeof(BaseRoles), BaseRoles.Operator)));
    option.AddPolicy(Enum.GetName(typeof(BaseRoles), BaseRoles.Customer), policy => policy.RequireRole(Enum.GetName(typeof(BaseRoles), BaseRoles.Customer)));
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Authentication/Signin");
    options.LogoutPath = new PathString("/Authentication/SignOut");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
    options.AccessDeniedPath = "/Authentication/Signin";
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

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
              );
    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
    
});

app.Run();
