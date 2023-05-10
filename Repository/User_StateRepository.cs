using Microsoft.EntityFrameworkCore;
using WEBAPIForUserAuthorization.DataCntxt;
using WEBAPIForUserAuthorization.Interface;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Repository
{
    public class User_StatesRepository : IUser_StateRepository
    {
        private readonly DataContext _context;

        public User_StatesRepository(DataContext context)
        {
            this._context = context;
        }

        public User_State GetState(int id)
        {
            return _context.User_States.Where(p => p.Id == id).FirstOrDefault();
        }

        public int GetStateCount()
        {
            return _context.User_States.OrderBy(p => p.Id).Count();
        }

        public ICollection<User_State> GetStates()
        {
            return _context.User_States.OrderBy(p => p.Id).ToList();
        }

        public bool StateExists(int stateId)
        {
            return _context.User_States.Any(p => p.Id == stateId);
        }
    }
}
