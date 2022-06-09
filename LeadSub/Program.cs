using BLL.Extensions;
using DAL.Context;
using Google.Apis.Auth.AspNetCore3;
using LeadSub.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string str = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddLeadSubDbContext(str);
builder.Services.AddLeadSubDataTransients();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.User.RequireUniqueEmail = true;
});


string identityConnection = builder.Configuration.GetConnectionString("IdentityConnection");

builder.Services.AddDbContext<AspNetIdentityContext>(options => options.UseSqlServer(identityConnection));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AspNetIdentityContext>()
    .AddEntityFrameworkStores<AspNetIdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSession();

builder.Services.AddAuthentication(o =>
{
    // This forces challenge results to be handled by Google OpenID Handler, so there's no
    // need to add an AccountController that emits challenges for Login.
    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // This forces forbid results to be handled by Google OpenID Handler, which checks if
    // extra scopes are required and does automatic incremental auth.
    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // Default scheme that will handle everything else.
    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
        .AddCookie()
        .AddGoogleOpenIdConnect(options =>
        {
            options.ClientId = "757118647530-4q3qngsol7lso7c44ptiifi8kci5f16e.apps.googleusercontent.com";
            options.ClientSecret = "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";
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
