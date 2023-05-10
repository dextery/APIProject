using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WEBAPIForUserAuthorization.DataCntxt;
using WEBAPIForUserAuthorization.Dto;
using WEBAPIForUserAuthorization.Interface;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        /* private readonly IUser_GroupRepository _groupRepo;
        private readonly IUser_StateRepository _stateRepo; */
        private readonly IMapper _mapper;

        //private readonly DataContext _dataContext;

        public UserController(IUserRepository userRepo, IMapper mapper)
        {
            this._userRepo = userRepo;
            this._mapper = mapper;
            //this._dataContext = dataContext;
        }

        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            //var users = _mapper.Map<List<UserDTO>>(_userRepo.GetUsers());
            var users = _userRepo.GetUsers();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepo.UserExists(id))
            {
                return NotFound();
            }
            //var user = _mapper.Map<UserDTO>(_userRepo.GetUser(id));
            var user = _userRepo.GetUser(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetUserCount(int groupId)
        {
            var count = _userRepo.GetUserCount(groupId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddUser(string login, string password, int groupId)
        {
            if (groupId == null)
            {
                return BadRequest(ModelState);
            }
            if (groupId == 2)
            {
                /*var count = _userRepo.GetUserCount(2);
                if (count > 0)
                {
                    return BadRequest(ModelState);
                } */

                var count = _userRepo.GetUsers().Where(p=> p.userInfo.User_Group_Id==groupId && p.userInfo.User_State_Id==1).Count();
                if (count > 0)
                {
                    ModelState.AddModelError("", "There is an already active admin!");
                    return StatusCode(422, ModelState);
                }
            }
            if (_userRepo.UserExistsLogin(login))
            {
                var a = DateTime.Now.ToUniversalTime();

                var conpUser = _userRepo.GetUserLogin(login);

                var b = conpUser.Created_Date.ToUniversalTime();

                var diffInSeconds = (b - a).TotalSeconds;

                if (Math.Abs(diffInSeconds) < 5)
                {
                    ModelState.AddModelError("", "Can't create a new user within the 5 seconds of creation of the original");
                    return StatusCode(422, ModelState);
                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addUser = await _userRepo.AddUser(login, password, groupId);

            if (!addUser)
            {
                ModelState.AddModelError("", "Something went wrong while adding user");
                return StatusCode(500, ModelState);
            }

            return Ok(addUser);
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest(ModelState);
            }
            if (userId != updatedUser.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepo.UserExists(userId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (updatedUser.User_Group_Id == 2)
            {
                if (_userRepo.GetUserCount(2) > 0)
                {
                    var admin = _userRepo.GetAdmin();
                    if (admin.Id != updatedUser.Id)
                    {
                        ModelState.AddModelError("", "You can't set a new admin while there's another admin present!");
                        return StatusCode(500, ModelState);
                    }
                }
            }
            var result = await _userRepo.UpdateUserData(updatedUser);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong when trying to update");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepo.UserExists(userId))
            {
                return NotFound();
            }
            var result = await _userRepo.DeleteUser(userId);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong when trying to block user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
