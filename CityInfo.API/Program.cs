using CityInfo.API;
using CityInfo.API.DbContexts;
using CityInfo.API.Repositories;
using CityInfo.API.Repositories.Interfaces;
using CityInfo.API.Services;
using CityInfo.API.Services.Interfaces;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

#region Serilog Config

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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region DbContext

builder.Services.AddDbContext<CityInfoDbContext>(options =>
{
    options.UseSqlite(builder.Configuration
        .GetConnectionString("DefaultConnection"));
});

#endregion

#region IoC

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddScoped<IMailService, GmailService>();
builder.Services.AddSingleton<CitiesDataStore>();
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

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