using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Interface
{
    public interface IUser_StateRepository
    {
        ICollection<User_State> GetStates();
        User_State GetState(int id);
        int GetStateCount();
        bool StateExists(int groupId);
    }
}
