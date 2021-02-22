using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository.IRepository
{
    public interface IRecipientRepository : IRepository<Recipient>
    {
        Task<bool> IsRecipientExists(string name);
    }
}
