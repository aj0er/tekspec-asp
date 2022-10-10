using ExampleForum.Models.View;
using ExampleForum.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleForum.Controllers
{
    public class BoardsController : Controller
    {

        private BoardService _boardService;
        public BoardsController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [Route("/{controller}")]
        public async Task<IActionResult> Index()
        {
            var boards = await _boardService.FetchBoards();
            return View(boards);
        }

        [Route("/{controller}/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var (board, threads) = await _boardService.FetchBoardAndThreads(id) ?? default;
            if (board == null)
                return NotFound();

            return View("ThreadList", new BoardThreadViewModel
            {
                Board = board,
                Threads = threads 
            });
        }

    }
}
