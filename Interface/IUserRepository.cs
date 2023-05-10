using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Interface
{
    public interface IUserRepository
    {
        ICollection<InfoContainer> GetUsers();
        InfoContainer GetUser(int id);
        User GetUserLogin(string login);
        User GetAdmin();
        int GetUserCount(int groupId);
        bool UserExists(int Id);
        bool UserExistsLogin(string login);
        Task<bool> UpdateUserData(User user);
        Task<bool> Save();
        Task<bool> DeleteUser(int Id);
        Task<bool> AddUser(string login, string password, int groupId);
    }
}
