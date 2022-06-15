using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EntityFramework.Data;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddDbContext<EntityFrameworkContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("EntityFrameworkContext") ?? throw new InvalidOperationException("Connection string 'EntityFrameworkContext' not found.")));
    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

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