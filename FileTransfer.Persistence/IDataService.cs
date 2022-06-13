using System.Collections.Generic;

namespace FileTransfer.Persistence
{
    public interface IDataService
    {
        void Connect();
        List<Person> GetPersons();
    }
}
