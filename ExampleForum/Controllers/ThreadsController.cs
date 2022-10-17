using ExampleForum.Areas.Identity.Data;
using ExampleForum.Models.Requests;
using ExampleForum.Models.Views;
using ExampleForum.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleForum.Controllers
{
    public class ThreadsController : Controller
    {

        private readonly UserManager<ExampleForumUser> _userManager;
        private readonly ThreadService _threadService;

        public ThreadsController(ThreadService threadService, UserManager<ExampleForumUser> userManager)
        {
            _threadService = threadService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return Content("yes");
        }

        [Route("/{controller}/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var (thread, posts) = await _threadService.FetchThreadAndPosts(id) ?? default;
            if (thread == null)
                return NotFound();

            return View(new ThreadViewModel
            {
                Thread = thread,
                Posts = posts,
            });    
        }


        [Route("/{controller}/create/{id}")]
        [HttpGet]
        public IActionResult CreateView()
        {
            return View("Create");
        }

        [Route("/{controller}/create/{id}")]
        [HttpPost]
        public async Task<IActionResult> Create(Guid id, [Bind("Title","Content")] CreateThreadRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var threadId = await _threadService.CreateThread(id, request, user);
            if (threadId == null)
                return BadRequest();

            return Redirect($"/threads/{threadId}");
        }

    }
}
