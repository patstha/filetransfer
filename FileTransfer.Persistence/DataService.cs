﻿using Microsoft.Extensions.Logging;

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

        public void Connect() => _log.LogDebug($"TODO: Write code to {nameof(Connect)} to Connection String {_connectionString}");

        public void Disconnect() => _log.LogDebug($"TODO: Write code to {nameof(Disconnect)} from Connection String {_connectionString} here.");

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            // Cleanup
            Disconnect();
        }
    }
}
