using CityInfo.API;
using CityInfo.API.Services;
using CityInfo.API.Services.Interfaces;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

#region Config Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo-logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region IoC

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddScoped<IMailService, GmailService>();
builder.Services.AddSingleton<CitiesDataStore>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();