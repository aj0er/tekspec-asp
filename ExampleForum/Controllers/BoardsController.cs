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

            return View("ThreadList", new BoardThreadViewModel
            {
                Board = board,
                Threads = threads
            });
        }

        public async Task<Models.Thread> Thread(Guid id)
        {
            return await _context.Thread
                .Include(t => t.Board)
                .FirstAsync(b => b.Id == id);
        }

    }
}
