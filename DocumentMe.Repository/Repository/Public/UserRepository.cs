using DocumentMe.DataAccessLayer.Database;
using DocumentMe.DataAccessLayer.Entity.Public;
using DocumentMe.Repository.IRepository.Public;
using Microsoft.EntityFrameworkCore;

namespace DocumentMe.Repository.Repository.Public
{
    public class UserRepository : IUserRepository
    {
        protected readonly DocumentMeDBContext _context;

        public UserRepository(DocumentMeDBContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _context.Users.Where(u => u.UserName == userName && u.IsActive).FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
