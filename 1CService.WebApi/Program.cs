using _1CService.Application;
using _1CService.Persistence;
using _1CService.Persistence.Repository;
using _1CService.Controllers;
using _1CService.Infrastructure;
using Microsoft.OpenApi.Models;

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


builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddAuthApplication();
builder.Services.Add1CApplication();


builder.Services.Add1CServiceRoles();


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "1C MicroService",
        Description = "Microservice of mobile client data exchange with 1C Service",
        TermsOfService = new Uri("https://api.prof4u.ru/swagger"),
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

await app.Initialize();

app.AddServiceEndpoints();

app.Urls.Add("https://0.0.0.0:7000");

app.Run();