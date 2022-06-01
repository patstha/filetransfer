using Microsoft.Extensions.Logging;

namespace FileTransfer.Persistence
{
    public class DataService: IDataService, System.IDisposable
    {
        private readonly ILogger<DataService> _log;
        private readonly string _connectionString;

        public DataService(
            ILogger<DataService> log,
            string connectionString)
        {
            _log = log;
            _connectionString = connectionString;
        }

        public void Connect() => _log.LogInformation($"TODO: Write code to connect to Connection String {_connectionString}");

        public void Disconnect() => _log.LogDebug($"TODO: Write code to disconnect from Connection String {_connectionString} here.");

        public void Dispose() => Disconnect();
    }
}
