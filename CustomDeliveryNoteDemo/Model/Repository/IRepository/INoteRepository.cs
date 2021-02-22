using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository.IRepository
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<Note> GetByNumberAsync(string noteNbr);
        Task<Note> GetWithAllPropertiesAsync(int id);
    }
}
