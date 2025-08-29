using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SingleRQuestionnaire.Models;
using SingleRQuestionnaire.Services;
using System.Security.Claims;



var builder = WebApplication.CreateBuilder(args);



// MongoDB için Guid serileştirme
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));


var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value,
        DatabaseName = builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value
    },
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.User.RequireUniqueEmail = true;
    }
};


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => { })
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
        builder.Configuration["MongoDbSettings:ConnectionString"],
        builder.Configuration["MongoDbSettings:DatabaseName"]
    )
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Mongo servis
builder.Services.AddSingleton<MongoDbService>();

builder.Services.AddTransient<IEmailSender, DummyEmailSender>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
// SignalR
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<QuestionnaireHub>("/questionnaireHub");

app.Run();
