using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTest.Sample
{
    public class SampleFactory
    {
        public Note GetSampleNote()
        {
            return new Note()
            {
                NoteNbr = "N00099"
            };
        }

        /// <summary>
        /// Returns a sample list of users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetSampleUserList()
        {
            List<User> userList = new List<User>
            {
                new User {Id = 1, Name = "admin", Password = "admin"},
                new User {Id = 2, Name = "boss", Password = "boss"},
                new User {Id = 2, Name = "employee", Password = "employee"},
            };

            return userList;
        }

        /// <summary>
        /// Returns a sample list of users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Recipient> GetSampleRecipientList()
        {
            List<Recipient> recipientList = new List<Recipient>
            {
                new Recipient {Id = 1, Code = "T-001", Name = "Kobe Bryant"},
                new Recipient {Id = 1, Code = "T-002", Name = "Michael Jordan"},
                new Recipient {Id = 1, Code = "T-003", Name = "Denis Rodman"},
            };

            return recipientList;
        }
    }
}
