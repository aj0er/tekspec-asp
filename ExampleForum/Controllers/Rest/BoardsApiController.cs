using ExampleForum.Models;
using ExampleForum.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleForum.Controllers.Rest
{
    [Route("/api/boards")]
    [ApiController]
    public class BoardsApiController : ControllerBase
    {

        private BoardService _boardService;

        public BoardsApiController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Board>> GetBoards()
        {
            return await _boardService.FetchBoards();
        }

        [HttpGet("{id}/threads")]
        public async Task<IActionResult> GetBoardAndThreads(Guid id)
        {
            var (board, threads) = await _boardService.FetchBoardAndThreads(id) ?? default;
            if (board == null)
                return NotFound();

            return Ok(new BoardContext
            {
                Board = board,
                Threads = threads
            });
        }

    }
}
