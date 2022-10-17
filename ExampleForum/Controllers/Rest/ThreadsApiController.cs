using ExampleForum.Areas.Identity.Data;
using ExampleForum.Models;
using ExampleForum.Models.Response;
using ExampleForum.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleForum.Controllers.Rest
{
    [Route("/api/threads")]
    [ApiController]
    public class ThreadsApiController : Controller
    {

        private ThreadService _threadService;

        public ThreadsApiController(ThreadService threadService)
        {
            _threadService = threadService;
        }

        [HttpGet("{id}/posts")]
        public async Task<IEnumerable<PostResponse>> GetPosts(Guid id)
        {
            var posts = await _threadService.FetchPostsByThread(id);
            return posts.Select(s => CreatePostResponse(s));
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
