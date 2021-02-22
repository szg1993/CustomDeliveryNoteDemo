using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        INoteRepository NoteRepo { get; }
        INoteLineRepository NoteLineRepo { get; }
        IRecipientRepository RecipientRepo { get; }
        IUserRepository UserRepo { get; }
        Task<bool> SaveAsync();
    }
}
