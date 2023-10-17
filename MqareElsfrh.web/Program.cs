using AuthorizeLibrary.Constants;
using AuthorizeLibrary.Data;
using DBModels.IdentityModel;
using AuthorizeLibrary.Seeding;
using jsonCultuerLocalizerLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

ApplicationDbContext.DBConnctionString = connectionString;

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();

// add loclization
builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, jsonStringLocalizerFactory>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(option =>
    {
        option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(jsonStringLocalizerFactory));
    });

var supportedLanguage = new[] { "en-US", "ar-EG" }; //this array of language

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = supportedLanguage.Select(c => new CultureInfo(c)).ToList();

    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedLanguage[0])
    .AddSupportedCultures(supportedLanguage)
    .AddSupportedUICultures(supportedLanguage);

app.UseRequestLocalization(localizationOptions);

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using var scop = app.Services.CreateScope();
var roleManger = scop.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await seedRoles.seedAsync(roleManger);

var userManger = scop.ServiceProvider.GetService<UserManager<AppUser>>();
await seedUsers.seedAsync(userManger
    ,roleManger
    ,new AppUser()
    {
        Email = $"{RoleConstants.Admin}@admin.com",
        UserName = $"{RoleConstants.Admin}@admin.com",
        FirstName = RoleConstants.Admin,
        LastName = "SYSUser",
        EmailConfirmed = true
    },
    RoleConstants.rolesList()
    );

app.Run();
