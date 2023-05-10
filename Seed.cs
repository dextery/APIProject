using WEBAPIForUserAuthorization.DataCntxt;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.User_Groups.Any())
            {
                var userGroups = new List<User_Group>()
                {
                    new User_Group()
                    {
                        Id= 1,
                        Code="User",
                        Description="Basic user, no additional privileges."
                    },
                    new User_Group()
                    {
                        Id= 2,
                        Code="Admin",
                        Description="A user with additional priviliges. There can be only one active admin at a time."
                    }
                };
                dataContext.User_Groups.AddRange(userGroups);
                dataContext.SaveChanges();
            }
            if(!dataContext.User_States.Any())
            {
                var states = new List<User_State>()
                {
                    new User_State()
                    {
                        Id= 1,
                        Code="Active",
                        Description="This user is active and seen by system."
                    },
                    new User_State()
                    {
                        Id= 2,
                        Code="Blocked",
                        Description="This user is blocked and thus ignored by the system."
                    }
                };
                dataContext.User_States.AddRange(states);
                dataContext.SaveChanges();
            }
            if(!dataContext.Users.Any())
            {
                var users = new List<User>
                {
                    new User()
                    {
                        Id= 1,
                        Login="Firstfirstoff",
                        Password="1",
                        Created_Date= DateTime.Now.ToUniversalTime(),
                        User_Group_Id = 2,
                        User_State_Id= 1
                    },
                    new User()
                    {
                        Id= 2,
                        Login="Secondova",
                        Password="2",
                        Created_Date= DateTime.Now.ToUniversalTime(),
                        User_Group_Id = 1,
                        User_State_Id= 1
                    },
                    new User()
                    {
                        Id = 3,
                        Login="Thirdovich",
                        Password="3",
                        Created_Date= DateTime.Now.ToUniversalTime(),
                        User_Group_Id = 1,
                        User_State_Id= 1
                    }
                };
                dataContext.Users.AddRange(users);
                dataContext.SaveChanges();
            }
        }
    }
}
