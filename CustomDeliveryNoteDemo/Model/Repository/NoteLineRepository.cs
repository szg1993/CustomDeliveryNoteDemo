using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class NoteLineRepository : Repository<NoteLine>, INoteLineRepository
    {
        #region Declaration

        private readonly CustomDeliveryNoteContext _db;

        #endregion

        #region Ctors

        public NoteLineRepository(CustomDeliveryNoteContext db) : base(db)
        {
            _db = db;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<NoteLine>> GetNoteLinesInNoteAsync(int noteId)
        {
            return await _db.NoteLine.Include(l => l.Note).Where(l => l.NoteId == noteId).ToListAsync();
        }

        public IEnumerable<NoteLine> GetNoteLinesInNote(int noteId)
        {
            return _db.NoteLine.Include(l => l.Note).Where(l => l.NoteId == noteId).ToList();
        }

        /// <summary>
        /// Return all data from the tables started from NoteLine.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NoteLine>> GetAllNoteLinesWithAllDataAsync()
        {
            return await _db.NoteLine
                .Include(l => l.Note)
                .Include(r => r.Note.Rec)
                .Include(u => u.Note.User)
                .ToListAsync();
        }

        #endregion
    }
}
