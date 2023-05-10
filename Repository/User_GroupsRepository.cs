using WEBAPIForUserAuthorization.DataCntxt;
using WEBAPIForUserAuthorization.Interface;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Repository
{
    public class User_GroupsRepository : IUser_GroupRepository
    {
        private readonly DataContext _context;
        public User_GroupsRepository(DataContext context)
        {
            this._context = context;
        }

        public User_Group GetGroup(int id)
        {
            return _context.User_Groups.Where(p => p.Id == id).FirstOrDefault();
        }

        public int GetGroupCount()
        {
            return _context.User_Groups.OrderBy(p=>p.Id).Count();
        }

        public ICollection<User_Group> GetGroups()
        {
            return _context.User_Groups.OrderBy(p => p.Id).ToList();
        }

        public bool GroupExists(int groupId)
        {
            return _context.User_Groups.Any(p => p.Id == groupId);
        }
    }
}
