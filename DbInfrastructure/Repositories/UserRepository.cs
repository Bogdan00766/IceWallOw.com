using Domain.IRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IceWallOwDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckPassword(string email, byte[] hash)
        {
            if(email == null) throw new ArgumentNullException("email cannot be null");
            if(hash == null) throw new ArgumentNullException("hash cannot be null");
            var user = FindByEmail(email);
            if(user == null) throw new Exception("User not found");

            if (user.Password == hash) return true;
            else return false;
        }

        public User? FindByEmail(string email)
        {
            return _dbContext.User.Where(x => x.EMail == email).FirstOrDefault();

        }
    }
}
