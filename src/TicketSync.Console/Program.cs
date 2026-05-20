using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TicketSync.Core.Interfaces;
using TicketSync.Data;
using TicketSync.Data.Repositories;

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
    
    // Add Dapper Context
    var connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");
    services.AddSingleton(new DapperContext(connectionString));
    
    // Add Repositories
    services.AddScoped<ITicketMappingRepository, TicketMappingRepository>();
    services.AddScoped<ISyncLogRepository, SyncLogRepository>();
    services.AddScoped<ITicketFieldSnapshotRepository, TicketFieldSnapshotRepository>();
    services.AddScoped<ISyncRetryRepository, SyncRetryRepository>();
    services.AddScoped<IFieldMappingConfigRepository, FieldMappingConfigRepository>();
    
    var serviceProvider = services.BuildServiceProvider();
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Aplikacija je pokrenuta...");
    logger.LogInformation("Connection string: {ConnectionString}", connectionString);
    
    // Test database connection
    var dapperContext = serviceProvider.GetRequiredService<DapperContext>();
    using (var connection = dapperContext.CreateConnection())
    {
        await connection.OpenAsync();
        logger.LogInformation("✓ Konekcija sa bazom podataka je uspe??na!");
        await connection.CloseAsync();
    }
    
    Console.WriteLine("\n===== TICKET SYNC APPLICATION =====");
    Console.WriteLine("✓ Database First + Dapper + Specifični Repositories");
    Console.WriteLine("\nFAZA 1: Osnovna struktura - ZAVR??ENA ✓");
    Console.WriteLine("\nAplikacija je sprema za FAZU 2: Integracija sa Jira API");
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
