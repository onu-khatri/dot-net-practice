namespace EntityFrameworkPractice;

public interface ICustomerDataService
{
    /// <summary>
    /// Applies pending database migrations.
    /// </summary>
    Task EnsureMigratedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new customer with the provided name and email.
    /// </summary>
    Task CreateCustomerAsync(string name, string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns customer names that start with the provided prefix.
    /// </summary>
    Task<List<string>> GetCustomerNamesStartingWithAsync(string prefix, CancellationToken cancellationToken = default);
}
