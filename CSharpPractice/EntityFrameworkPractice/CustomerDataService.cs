using EntityFrameworkPractice.DbContext;
using EntityFrameworkPractice.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPractice;

public sealed class CustomerDataService : ICustomerDataService
{
    private readonly AppDbContext _dbContext;

    public CustomerDataService(AppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        _dbContext = dbContext;
    }

    public Task EnsureMigratedAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.MigrateAsync(cancellationToken);
    }

    public async Task CreateCustomerAsync(string name, string email, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
        }

        var customer = new Customer
        {
            Name = name,
            Email = email
        };

        await _dbContext.Customers.AddAsync(customer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<string>> GetCustomerNamesStartingWithAsync(string prefix, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(prefix))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(prefix));
        }

        return _dbContext.Customers
            .AsNoTracking()
            .Where(customer => customer.Id > 1)
            .OrderBy(customer => customer.Id)
            .Select(customer => customer.Name)
            .Where(name => name.StartsWith(prefix))
            .ToListAsync(cancellationToken);
    }
}
