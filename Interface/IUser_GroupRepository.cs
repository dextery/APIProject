using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Interface
{
    public interface IUser_GroupRepository
    {
        ICollection<User_Group> GetGroups();
        User_Group GetGroup(int id);
        int GetGroupCount();
        bool GroupExists(int groupId);
    }
}
