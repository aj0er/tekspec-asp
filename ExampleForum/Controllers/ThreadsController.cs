using ExampleForum.Data;
using ExampleForum.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleForum.Controllers
{
    public class ThreadsController : Controller
    {

        private ExampleForumContext _context;
        public ThreadsController(ExampleForumContext context)
        {
            _context = context;
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

    }
}
