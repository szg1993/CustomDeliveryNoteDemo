using Model.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomDeliveryNoteContext _db;

        public INoteRepository NoteRepo { get; private set; }
        public INoteLineRepository NoteLineRepo { get; private set; }
        public IRecipientRepository RecipientRepo { get; private set; }
        public IUserRepository UserRepo { get; private set; }

        public UnitOfWork(CustomDeliveryNoteContext db)
        {
            _db = db;
            NoteRepo = new NoteRepository(_db);
            NoteLineRepo = new NoteLineRepository(_db);
            RecipientRepo = new RecipientRepository(_db);
            UserRepo = new UserRepository(_db);
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
