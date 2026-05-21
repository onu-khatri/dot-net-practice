namespace EntityFrameworkPractice.Testers;

public sealed class CustomerTester
{
    private readonly CustomerDataService _customerDataService;

    public CustomerTester(CustomerDataService customerDataService)
    {
        ArgumentNullException.ThrowIfNull(customerDataService);
        _customerDataService = customerDataService;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await _customerDataService.EnsureMigratedAsync(cancellationToken);

        var email = $"Montu{DateTime.UtcNow.Ticks}@email.com";
        await _customerDataService.CreateCustomerAsync("John Doe", email, cancellationToken);

        var names = await _customerDataService.GetCustomerNamesStartingWithAsync("J", cancellationToken);

        var count = names.Count;
        Console.WriteLine($"Total names that start with J: {count}");

        var nameSet = new HashSet<string>(names, StringComparer.Ordinal);

        foreach (var name in new[] { "ab", "bc", "cd" })
        {
            var isExists = nameSet.Contains(name);
            Console.WriteLine($"Customer Name: {name}, Exists: {isExists}");
        }
    }
}
