using ExampleForum.Areas.Identity.Data;
using ExampleForum.Models;
using ExampleForum.Models.Requests;
using ExampleForum.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleForum.Controllers.Rest
{
    [Route("/api/boards")]
    [ApiController]
    public class BoardsApiController : Controller
    {

        private BoardService _boardService;
        private ThreadService _threadService;
        private readonly UserManager<ExampleForumUser> _userManager;

        public BoardsApiController(BoardService boardService, ThreadService threadService, UserManager<ExampleForumUser> userManager)
        {
            _boardService = boardService;
            _threadService = threadService;
            _userManager = userManager;
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

        [HttpPost("{id}/threads")]
        public async Task<IActionResult> CreateThread(Guid id, [FromBody] CreateThreadRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var threadId = await _threadService.CreateThread(id, request, user);
            if(threadId != null)
            {
                return Json(threadId.ToString());
            } else
            {
                return BadRequest();
            }
        }

        
    }
}
