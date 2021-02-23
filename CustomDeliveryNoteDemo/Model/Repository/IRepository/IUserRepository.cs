using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByNameAndPasswordAsync(string userName, string password);
        User GetUserByNameAndPassword(string userName, string password);
    }
}
