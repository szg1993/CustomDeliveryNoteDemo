using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class RecipientRepository : Repository<Recipient>, IRecipientRepository
    {
        #region Declaration

        private readonly CustomDeliveryNoteContext _db;

        #endregion

        #region Ctors

        public RecipientRepository(CustomDeliveryNoteContext db) : base(db)
        {
            _db = db;
        }

        #endregion

        #region Methods

        public async Task<bool> IsRecipientExists(string name)
        {
            return await _db.Recipient.FirstOrDefaultAsync(r => r.Name.Trim().ToUpper() == name.Trim().ToUpper()) != null;
        }

        #endregion
    }
}
