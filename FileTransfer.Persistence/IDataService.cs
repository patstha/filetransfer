using System.Collections.Generic;

namespace FileTransfer.Persistence
{
    public interface IDataService
    {
        void Connect();
        List<Person> GetPersons();
        List<Person> GetPersonsDapper();
        void CreateStateLookupTable();
        void DeleteStateLookupTable();
        void InsertStateLookupTable();
        List<LookupState> GetStateLookupTable();
    }
}
