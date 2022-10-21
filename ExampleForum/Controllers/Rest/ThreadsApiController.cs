using ExampleForum.Areas.Identity.Data;
using ExampleForum.Models;
using ExampleForum.Models.Requests;
using ExampleForum.Models.Response;
using ExampleForum.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleForum.Controllers.Rest
{
    [Route("/api/threads")]
    [ApiController]
    public class ThreadsApiController : Controller
    {

        private ThreadService _threadService;
        private PostService _postService;
        private readonly UserManager<ExampleForumUser> _userManager;

        public ThreadsApiController(ThreadService threadService, PostService postService, UserManager<ExampleForumUser> userManager)
        {
            _threadService = threadService;
            _postService = postService;
            _userManager = userManager;
        }

        [HttpGet("{id}/posts")]
        public async Task<IActionResult> GetPosts(Guid id)
        {
            var (thread, posts) = await _threadService.FetchThreadAndPosts(id) ?? default;
            if (thread == null)
                return NotFound();

            return Ok(new ThreadContext
            {
                Thread = thread,
                Posts = posts.Select(s => CreatePostResponse(s))
            });
        }

        [HttpPost("{threadId}/posts")]
        public async Task<IActionResult> CreatePost(Guid threadId, CreatePostRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var thread = await _threadService.FetchThreadById(threadId);
            if (thread == null)
                return NotFound();

            if(await _postService.CreatePost(thread.Id, request, user))
            {
                return await _postService.GetPosts();
            } else
            {
                return BadRequest();
            }
        }

        private PostResponse CreatePostResponse(Post post)
        {
            return new PostResponse
            {
                Id = post.Id,
                Content = post.Content,
                User = CreateUserResponse(post.Author),
            };
        }

        private UserResponse CreateUserResponse(ExampleForumUser user)
        {
            return new UserResponse { Id = user.Id, DisplayName = user.UserName };
        }

    }
}
