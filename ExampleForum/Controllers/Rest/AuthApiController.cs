using ExampleForum.Areas.Identity.Data;
using ExampleForum.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleForum.Controllers.Rest
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthApiController : Controller
    {

        private readonly UserManager<ExampleForumUser> _userManager;

        public AuthApiController(UserManager<ExampleForumUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            return Json(new AuthInfoResponse
            {
                Id = user.Id,
                UserName = user.UserName
            });
        }

    }
}
