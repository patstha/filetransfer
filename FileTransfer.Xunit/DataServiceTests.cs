using FileTransfer.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FileTransfer.Xunit;
public class DataServiceTests : System.IDisposable
{
    DataService service;
    public DataServiceTests()
    {
        var log = Mock.Of<ILogger<DataService>>();
        var inMemorySettings = new System.Collections.Generic.Dictionary<string, string> {
            {"TopLevelKey", "TopLevelValue"},
            {"ConnectionStrings:DefaultConnection", "DataSource:app.db;Cache=Shared"},
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        service = new(log, connectionString);
    }
    public void Dispose()
    {
        service.Dispose();
    }

    [Fact]
    public void TestFreebie() => Assert.NotNull(service);

    [Fact]
    public void TestConnect() => service.Connect();
}
