using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository.IRepository
{
    public interface INoteLineRepository : IRepository<NoteLine>
    {
        Task<IEnumerable<NoteLine>> GetNoteLinesInNoteAsync(int noteId);
        IEnumerable<NoteLine> GetNoteLinesInNote(int noteId);
        Task<IEnumerable<NoteLine>> GetAllNoteLinesWithAllDataAsync();
    }
}
