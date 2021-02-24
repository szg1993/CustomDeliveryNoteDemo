using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelTest.Sample
{
    public class SampleFactory
    {
        /// <summary>
        /// Returns a sample list from notes for testing purposes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Note> GetSampleNoteList()
        {
            List<Note> noteList = new List<Note>
            {
                new Note {Id = 1, NoteNbr = "N00001"},
                new Note {Id = 2, NoteNbr = "N00002"},
                new Note {Id = 3, NoteNbr = "N00003"},
                new Note {Id = 4, NoteNbr = "N00004"},
                new Note {Id = 5, NoteNbr = "N00005"},
            };

            return noteList;
        }

        /// <summary>
        /// Returns a sample list from note lines for testing purposes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NoteLine> GetSampleNoteLineList()
        {
            List<NoteLine> noteLineList = new List<NoteLine>
            {
                new NoteLine {Id = 1, NoteId = 1, PartCode = "ABC-001" },
                new NoteLine {Id = 2, NoteId = 1, PartCode = "ABC-002" },
                new NoteLine {Id = 3, NoteId = 2, PartCode = "ABC-003" },
                new NoteLine {Id = 4, NoteId = 2, PartCode = "ABC-004" },
                new NoteLine {Id = 5, NoteId = 3, PartCode = "ABC-005" },
                new NoteLine {Id = 6, NoteId = 4, PartCode = "ABC-006" },
            };

            return noteLineList;
        }

        /// <summary>
        /// Returns a sample list of users for testing purposes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetSampleUserList()
        {
            List<User> userList = new List<User>
            {
                new User {Id = 1, Name = "admin", Password = "admin"},
                new User {Id = 2, Name = "boss", Password = "boss"},
                new User {Id = 3, Name = "employee", Password = "employee"},
            };

            return userList;
        }

        /// <summary>
        /// Returns a sample list of recipients for testing purposes.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Recipient> GetSampleRecipientList()
        {
            List<Recipient> recipientList = new List<Recipient>
            {
                new Recipient {Id = 1, Code = "T-001", Name = "Kobe Bryant"},
                new Recipient {Id = 2, Code = "T-002", Name = "Michael Jordan"},
                new Recipient {Id = 3, Code = "T-003", Name = "Denis Rodman"},
            };

            return recipientList;
        }
    }
}
