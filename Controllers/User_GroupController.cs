using Microsoft.AspNetCore.Mvc;
using WEBAPIForUserAuthorization.Interface;
using WEBAPIForUserAuthorization.Models;

namespace WEBAPIForUserAuthorization.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class User_GroupController : Controller
    {
        private readonly IUser_GroupRepository _userGroupRepo;

        public User_GroupController(IUser_GroupRepository userGroupRepo)
        {
            this._userGroupRepo = userGroupRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User_Group>))]
        public IActionResult GetUserGroups()
        {
            var userGroups = _userGroupRepo.GetGroups();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userGroups);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User_Group))]
        [ProducesResponseType(400)]
        public IActionResult GetUserGroup(int id)
        {
            if (!_userGroupRepo.GroupExists(id))
            {
                return NotFound();
            }
            var userGroup = _userGroupRepo.GetGroup(id);

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
            var count = _userGroupRepo.GetGroupCount();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(count);
        }
    }
}
