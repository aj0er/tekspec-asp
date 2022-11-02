using ExampleForum.Areas.Identity.Data;
using ExampleForum.Models;
using ExampleForum.Models.Requests;
using ExampleForum.Models.Response;
using ExampleForum.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thread = ExampleForum.Models.Thread;

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
                Thread = CreateThreadResponse(thread),
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
                var posts = await _postService.FetchPostsByThread(thread.Id);
                return Json(posts.Select(p => CreatePostResponse(p)));
            } else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThread(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            return await _threadService.DeleteThread(id, user) ? Ok() : NotFound();
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

        private ThreadResponse CreateThreadResponse(Thread thread)
        {
            return new ThreadResponse
            {
                Id = thread.Id,
                Name = thread.Name,
                Board = thread.BoardId,
                Author = thread.AuthorId
            };
        }

        private UserResponse CreateUserResponse(ExampleForumUser user)
        {
            return new UserResponse { Id = user.Id, DisplayName = user.UserName };
        }

    }
}
