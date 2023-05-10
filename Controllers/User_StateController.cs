using Microsoft.AspNetCore.Mvc;
using WEBAPIForUserAuthorization.Interface;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class User_StateController : Controller
    {
        private readonly IUser_StateRepository _userStateRepo;

        public User_StateController(IUser_StateRepository userStateRepo)
        {
            this._userStateRepo = userStateRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User_Group>))]
        public IActionResult GetUserStates()
        {
            var userGroups = _userStateRepo.GetStates();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userGroups);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User_Group))]
        [ProducesResponseType(400)]
        public IActionResult GetUserState(int id)
        {
            if (!_userStateRepo.StateExists(id))
            {
                return NotFound();
            }
            var userGroup = _userStateRepo.GetState(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userGroup);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetUserGroupCount()
        {
            var count = _userStateRepo.GetStateCount();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(count);
        }
    }
}
