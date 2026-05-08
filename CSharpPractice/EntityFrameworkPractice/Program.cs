using EntityFrameworkPractice.DbContext;
using EntityFrameworkPractice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(connectionString)
    .Options;

await using var dbContext = new AppDbContext(dbContextOptions);

await dbContext.Database.MigrateAsync();

var customer = new Customer
{
    Name = "John Doe",
    Email = "Montu@email.com,"
};

await dbContext.Customers.AddAsync(customer);
await dbContext.SaveChangesAsync();

var customers = dbContext.Customers.Where(t => t.Id > 1);//.ToListAsync();

foreach (var c in customers)
{
    Console.WriteLine($"Customer ID: {c.Id}, Name: {c.Name}, Email: {c.Email}");
}
