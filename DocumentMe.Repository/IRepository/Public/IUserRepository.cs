using DocumentMe.DataAccessLayer.Entity.Public;

namespace DocumentMe.Repository.IRepository.Public
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserName(string userName);
        Task CreateUser(User user);
    }
}
