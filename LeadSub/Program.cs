using BLL.Extensions;
using DAL.Context;

using LeadSub.APIConfig;
using LeadSub.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string str = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddLeadSubDbContext(str);
builder.Services.AddLeadSubDataTransients();

string identityConnection = builder.Configuration.GetConnectionString("IdentityConnection");
builder.Services.ConfigureIdentityOptions(identityConnection);

//builder.Services.AddLocalization(o => { o.ResourcesPath = "Resources"; });
//builder.Services.Configure<RequestLocalizationOptions>(options => {
//    List<CultureInfo> supportedCultures = new List<CultureInfo>
//    {
//        new CultureInfo("en-US"),
//        new CultureInfo("uk-UK")
//    };
//    options.DefaultRequestCulture = new RequestCulture("uk-UK");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = AuthOptions.ISSUER,
                       ValidateAudience = true,
                       ValidAudience = AuthOptions.AUDIENCE,
                       ValidateLifetime = true,
                       IssuerSigningKey = AuthOptions.GetSymetricKey(),
                       ValidateIssuerSigningKey = true
                   };
               });

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseRequestLocalization();

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
