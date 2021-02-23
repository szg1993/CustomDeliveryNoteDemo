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

        public User GetUserByNameAndPassword(string userName, string password)
        {
            return _db.User.FirstOrDefault(u => u.Name == userName && u.Password == password);
        }

        public async Task<User> GetUserByNameAndPasswordAsync(string userName, string password)
        {
            return await _db.User.FirstOrDefaultAsync(u => u.Name == userName && u.Password == password);
        }

        #endregion
    }
}
