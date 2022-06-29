using FileTransfer.Persistence;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
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
            {"ConnectionStrings:DefaultConnection", "Server=kfzks4a1bmk8.eu-central-2.psdb.cloud;Database=duck;user=6inzb06d1l62;password=pscale_pw_Eyp3wANw-LK8HGuvG2m-xr0lIlzBK_HnAL-Yij_7nwY;SslMode=VerifyFull;"},
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

    [Fact]
    public void TestGetLookupStatesDapper_ShouldReturnNotNull()
    {
        service.DeleteStateLookupTable();
        service.CreateStateLookupTable();
        service.InsertStateLookupTable();
        var states = service.GetStateLookupTable();

        states.Count.Should().BePositive();
        states.Where(x => x.Name == "Alaska").First().Capital.Should().Be("Anchorage");

        service.DeleteStateLookupTable();
    }
}
