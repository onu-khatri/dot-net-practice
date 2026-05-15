using EntityFrameworkPractice.DbContext;
using EntityFrameworkPractice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(connectionString)
    .Options;

await using var dbContext = new AppDbContext(dbContextOptions);

var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

if (pendingMigrations.Any())
{
    await dbContext.Database.MigrateAsync();
}

var customer = new Customer
{
    Name = "John Doe",
    Email = $"Montu{DateTime.Now.Ticks}@email.com"
};

await dbContext.Customers.AddAsync(customer);
await dbContext.SaveChangesAsync();

var customers = await dbContext.Customers
    .AsNoTracking()
    .Where(t => t.Id > 1)
    .OrderBy(t => t.Id)
    .ToListAsync();

foreach (var c in customers)
{
    Console.WriteLine($"Customer ID: {c.Id}, Name: {c.Name}, Email: {c.Email}");
}

// dotnet tool install --global dotnet-ef
// dotnet ef migrations add InitialCreate
// dotnet ef database update
// dotnet ef migrations remove

