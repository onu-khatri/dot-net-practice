namespace EntityFrameworkPractice.Testers;

public interface ICustomerTester
{
    /// <summary>
    /// Executes customer data workflow checks.
    /// </summary>
    Task RunAsync(CancellationToken cancellationToken = default);
}
