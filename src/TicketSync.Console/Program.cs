using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TicketSync.Core.Interfaces;
using TicketSync.Infrastructure.Data;
using TicketSync.Infrastructure.Repositories;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/ticketsync-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    // Setup Dependency Injection
    var services = new ServiceCollection();
    
    // Add configuration
    services.AddSingleton<IConfiguration>(configuration);
    
    // Add logging
    services.AddLogging(config =>
    {
        config.ClearProviders();
        config.AddSerilog();
    });
    
    // Add DbContext
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString, sqlOptions =>
            sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        )
    );
    
    // Add Repositories
    services.AddScoped<ITicketMappingRepository, TicketMappingRepository>();
    services.AddScoped<ISyncLogRepository, SyncLogRepository>();
    services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    
    var serviceProvider = services.BuildServiceProvider();
    
    // Apply migrations and create database
    using (var scope = serviceProvider.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        logger.LogInformation("Primenjujem migracije baze podataka...");
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("Migracije su uspešno primenjene.");
    }
    
    Console.WriteLine("\n===== TICKET SYNC APPLICATION =====");
    Console.WriteLine("Baza podataka je uspešno inicijalizovana.");
    Console.WriteLine("\nFAZA 1: Osnovna struktura - ZAVRŠENA ✓");
    Console.WriteLine("\nDobrodošli u TicketSync aplikaciju!");
    Console.WriteLine("Sledeća faza: Integracija sa Jira API");
    Console.WriteLine("\nPritisnite bilo koju tasterku za izlaz...");
    Console.ReadKey();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Greška pri pokretanju aplikacije");
    Console.WriteLine($"GREŠKA: {ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}
