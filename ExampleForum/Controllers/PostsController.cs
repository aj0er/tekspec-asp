using ExampleForum.Models.Requests;
using ExampleForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ExampleForum.Areas.Identity.Data;
using ExampleForum.Services;

namespace ExampleForum.Controllers
{
    public class PostsController : Controller
    {

        private readonly UserManager<ExampleForumUser> _userManager;
        private readonly PostService _postService;

        public PostsController(PostService postService, UserManager<ExampleForumUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("ThreadId,Content")] CreatePostRequest request)
        {
            var author = await _userManager.GetUserAsync(User);
            if (author == null)
                return Unauthorized();

            if (await _postService.CreatePost(request, author))
            {
                return Redirect($"/Threads/{request.ThreadId}");
            } else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{controller}/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            return await _postService.DeletePost(id, user) 
                ? Ok() 
                : BadRequest();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("{controller}/{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Content")] Post post)
        {
            if (id != post.Id)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            return await _postService.EditPost(post, user)
                ? Ok() 
                : BadRequest();
        }

    }
}
