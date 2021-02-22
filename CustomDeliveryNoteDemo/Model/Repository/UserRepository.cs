using Model.Models;
using Model.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        #region Declaration

        private readonly CustomDeliveryNoteContext _db;

        #endregion

        #region Ctors

        public UserRepository(CustomDeliveryNoteContext db) : base(db)
        {
            _db = db;
        }

        #endregion

        #region Methods

        #endregion
    }
}
