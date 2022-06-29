using FileTransfer.Persistence;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FileTransfer.Xunit;
public class DataServiceTests
{
    DataService service;
    public DataServiceTests()
    {
        var log = Mock.Of<ILogger<DataService>>();
        var inMemorySettings = new System.Collections.Generic.Dictionary<string, string> {
            {"TopLevelKey", "TopLevelValue"},
            {"ConnectionStrings:DefaultConnection", "Server=kfzks4a1bmk8.eu-central-2.psdb.cloud;Database=duck;user=rgk2vo8bc118;password=pscale_pw_zSOzHujvJHCLUpDd-dBYiZzLorGVYhvGzoXErlG3FEo;SslMode=VerifyFull;"},
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        service = new(log, connectionString);
    }

    [Fact]
    public void TestFreebie() => Assert.NotNull(service);

    [Fact]
    public void TestConnect()
    {
        service.Connect();
        Assert.NotNull(service);
    }

    [Fact]
    public void TestGetPersonsDapper_ShouldReturnNotNull()
    {
        var persons = service.GetPersonsDapper();
        persons.Count.Should().BePositive();
    }

}
