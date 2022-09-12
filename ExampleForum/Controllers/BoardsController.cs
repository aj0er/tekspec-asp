using ExampleForum.Data;
using ExampleForum.Models;
using ExampleForum.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExampleForum.Controllers
{
    public class BoardsController : Controller
    {

        private ExampleForumContext _context;
        public BoardsController(ExampleForumContext context)
        {
            _context = context;
        }

        [Route("/{controller}")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Board.ToListAsync());
        }

        [Route("/{controller}/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var board = await _context.Board.FirstAsync(b => b.Id == id);
            var threads = await _context.Thread.Where(b => b.BoardId == board.Id).ToListAsync();

            return View("BoardInfo", new BoardThreadViewModel
            {
                Board = board,
                Threads = threads
            });
        }

        [Route("/{controller}/thread")]
        public async Task<object> Threade()
        {
            var thread = new Models.Thread();
            thread.Name = "Första trådennnn!";
            thread.Board = await _context.Board.FirstAsync(b => b.Name == "Generellt");
            _context.Thread.Add(thread);
            await _context.SaveChangesAsync();
            return thread;
        }
        public async Task<Models.Thread> Thread(Guid id)
        {
            return await _context.Thread.Include(t => t.Board).FirstAsync(b => b.Id == id);
        }
    }
}
