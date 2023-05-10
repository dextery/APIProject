using WEBAPIForUserAuthorization.DataCntxt;
using WEBAPIForUserAuthorization.Interface;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        { 
            this._context=context;
        }

        public ICollection<InfoContainer> GetUsers()
        {
            var users = new List<InfoContainer>();

            var uslist = _context.Users.OrderBy(p=> p.Id).ToList();

            foreach(var user in uslist)
            {
                var container = new InfoContainer()
                {
                    userInfo = user,
                    user_State = _context.User_States.Where(p => p.Id == user.User_State_Id).FirstOrDefault(),
                    user_Group = _context.User_Groups.Where(p => p.Id == user.User_Group_Id).FirstOrDefault()
                };
                users.Add(container);
            }
            return users;
        }

        public InfoContainer GetUser(int id)
        {
            var user = _context.Users.Where(p => p.Id == id).FirstOrDefault();

            var container = new InfoContainer()
            {
                userInfo = user,
                user_Group = _context.User_Groups.Where(p => p.Id == user.User_Group_Id).FirstOrDefault(),
                user_State = _context.User_States.Where(p => p.Id == user.User_State_Id).FirstOrDefault(),
            };

            return container;
        }

        public User GetAdmin() 
        {
            var admin = _context.Users.Where(p=> p.User_Group_Id==2).FirstOrDefault();
            return admin;
        }

        public int GetUserCount(int groupId)
        {
            return _context.Users.Where(p=>p.User_Group_Id == groupId && p.User_State_Id==1).Count();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(p => p.Id == id && p.User_State_Id == 1);
        }

        public async Task<bool> AddUser(string login, string password, int groupId)
        {
            int uid = _context.Users.OrderByDescending(p => p.Id).First().Id;

            //var comparison = 0;

            /*if (groupId == 2)
            {
                comparison = GetUserCount(groupId);

                if (comparison > 0)
                {
                    groupId = 0;
                }
            } */

            var user = new User
            {
                Id = uid+1,
                User_State_Id = 1,
                User_Group_Id = groupId,
                Login = login,
                Password = password,
                Created_Date = DateTime.Now.ToUniversalTime(),
            };

            await _context.Users.AddAsync(user);

            Thread.Sleep(5000);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateUserData(User user)
        {

            _context.Users.Update(user);
            return await Save();
        }

        public async Task<bool> DeleteUser(int Id)
        {
            var user = _context.Users.Where(p=> p.Id == Id).FirstOrDefault();

            user.User_State_Id = 2;

            return await UpdateUserData(user);
        }

        public bool UserExistsLogin(string login)
        {
            return _context.Users.Any(p => p.Login == login && p.User_State_Id == 1);
        }

        public User GetUserLogin(string login)
        {
            return _context.Users.Where(p => p.Login == login).FirstOrDefault();
        }
    }
}
