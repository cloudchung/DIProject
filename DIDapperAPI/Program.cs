using Serilog;
using Serilog.Events;
using System;
using DIDapperAPI.service;
using Microsoft.EntityFrameworkCore;
using DIDapperAPI.Model;
using DIDapperAPI;
using System.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("DapperContext");


Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Information()
    .MinimumLevel.Warning()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

//Logging使用設定檔
//var configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.Development.json")
//    .Build();
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(configuration)
//    .CreateLogger();
//Log.Information("Hello");

try
{
    //Log.Information("Starting Web Host");
    builder.Services.AddSingleton(new Conn() { ConnectionString = builder.Configuration["ConnectionStrings:DapperContext"] });
    // Add services to the container.

    //使用自訂appsetting參數
    var setingConfig = new AppSetting();
    builder.Configuration.Bind(setingConfig);
    var rootConfig = new TestSection();
    builder.Configuration.GetSection("TestSection").Bind(rootConfig);
    builder.Services.AddSingleton(rootConfig);
    builder.Services.AddSingleton(setingConfig);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Warning()
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341"));

    builder.Services.AddRazorPages();
    builder.Services.AddScoped<MovieService>();
    builder.Services.AddMemoryCache(); //添加MemoryCache組件

    builder.Services.AddControllers();

    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<FirstMiddleware>();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    Log.CloseAndFlush(); // 非常重要的一段！
}