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
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        #region Declaration

        private readonly CustomDeliveryNoteContext _db;

        #endregion

        #region Ctors

        public NoteRepository(CustomDeliveryNoteContext db) : base(db)
        {
            _db = db;
        }

        #endregion

        #region Methods

        public async Task<Note> GetByNumberAsync(string noteNbr)
        {
            return await _db.Note.FirstOrDefaultAsync(n => n.NoteNbr == noteNbr);
        }

        public async Task<Note> GetWithAllPropertiesAsync(int id)
        {
            IQueryable<Note> query = _db.Set<Note>()
                .Include("Recipient")
                .Include("User");

            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<string> GetLastNoteNumberAsync()
        {
            return (await _db.Note.OrderByDescending(x => x.Id).FirstOrDefaultAsync()).NoteNbr;
        }

        #endregion
    }
}
