using System;

namespace FileTransfer.Persistence
{
    public class Person
    {
        public UInt64 Id { get; set; }
        public string LegalName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}