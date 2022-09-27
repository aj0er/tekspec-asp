using ExampleForum.Areas.Identity.Data;
using ExampleForum.Data;
using ExampleForum.Models.Requests;
using ExampleForum.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleForum.Controllers
{
    public class ThreadsController : Controller
    {

        private ExampleForumContext _context;
        private ILogger<ThreadsController> _logger;
        private readonly UserManager<ExampleForumUser> _userManager;

        public ThreadsController(ExampleForumContext context, ILogger<ThreadsController> logger, UserManager<ExampleForumUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/{controller}/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var thread = await _context.Thread.FirstAsync(b => b.Id == id);
            var posts  = await _context.Post
                .Where(b => b.ThreadId == thread.Id)
                .Include(p => p.Author)
                .OrderBy(p => p.Created)
                .ToListAsync();

            return View(new ThreadViewModel
            {
                Thread = thread,
                Posts = posts,
            });    
        }


        [Route("/{controller}/create/{id}")]
        [HttpGet]
        public async Task<IActionResult> CreateView(Guid id)
        {
            return View("Create");
        }

        [Route("/{controller}/create/{id}")]
        [HttpPost]
        public async Task<IActionResult> Create(Guid id, [Bind("Title","Content")] CreateThreadRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var threadId = Guid.NewGuid();

            var thread = new Models.Thread();
            _logger.LogInformation(request.Title);
            _logger.LogInformation(request.Content);
            thread.Id = threadId;
            thread.Name = request.Title;
            thread.BoardId = id;

            var post = new Models.Post
            {
                Id = Guid.NewGuid(),
                ThreadId = threadId,
                Author = user,
                Content = request.Content,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            _context.Post.Add(post);
            _context.Thread.Add(thread);
            await _context.SaveChangesAsync();

            return Redirect($"/threads/{threadId}");
        }

    }
}
