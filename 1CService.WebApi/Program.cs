using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using _1CService.Application;
using _1CService.Application.DTO;
using _1CService.Persistence;
using _1CService.Persistence.Repository;
using _1CService.Controllers;
using _1CService.Utilities;
using _1CService.Infrastructure;
using _1CService.Persistence.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration["Kestrel:Certificates:Default:Path"] = "cert.pem";
//builder.Configuration["Kestrel:Certificates:Default:KeyPath"] = "key.pem";
/*
 * builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        var certPath = Path.Combine(builder.Environment.ContentRootPath, "cert.pem");
        var keyPath = Path.Combine(builder.Environment.ContentRootPath, "key.pem");

        httpsOptions.ServerCertificate = X509Certificate2.CreateFromPemFile(certPath, 
                                         keyPath);
    });
});
 */

builder.Services.AddSingleton(new KeyManager());
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence();


builder.Services.AddDbContext<AppUserDbContext>(c => c.UseInMemoryDatabase("my_db"));
//builder.Services.AddDbContext<AppUserDbContext>(options =>
//options.UseSqlServer(builder.Build().Configuration.GetConnectionString("DbConnection")));


builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppUserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(option=>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };

        o.Configuration = new OpenIdConnectConfiguration()
        {
            SigningKeys =
            {
                new RsaSecurityKey(new KeyManager().RsaKey)
            },
        };
        o.MapInboundClaims = false;
    });

//builder.Services.AddTransient<IAuthenticateService, AuthenticationService>();

builder.Services.AddRoles();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

await app.AddDefaultUserAndRole();

app.AddEndpoints();

app.Urls.Add("https://0.0.0.0:7000");
//app.Urls.Add("http://0.0.0.0:5000");

app.Run();