using EntityFrameworkPractice;
using EntityFrameworkPractice.DbContext;
using EntityFrameworkPractice.Testers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration);
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
services.AddScoped<CustomerDataService>();
services.AddScoped<CustomerTester>();

await using var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
{
    ValidateOnBuild = true,
    ValidateScopes = true
});

await using var scope = serviceProvider.CreateAsyncScope();
var tester = scope.ServiceProvider.GetRequiredService<CustomerTester>();
await tester.RunAsync();

// dotnet tool install --global dotnet-ef
// dotnet ef migrations add InitialCreate
// dotnet ef database update
// dotnet ef migrations remove

// dependency injection in .net
// Inversion of Control (IoC) container
