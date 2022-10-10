using ExampleForum.Data;
using ExampleForum.Models;
using Microsoft.EntityFrameworkCore;
using Thread = ExampleForum.Models.Thread;

namespace ExampleForum.Services
{
    /// <summary>
    /// Tjänst som hanterar CRUD för (<see cref="Board"/>).
    /// </summary>
    public class BoardService
    {

        private readonly ExampleForumContext _db;

        public BoardService(ExampleForumContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Hämtar alla tavlor från databasen.
        /// </summary>
        /// <returns>En lista med tavlor.</returns>
        public async Task<List<Board>> FetchBoards()
        {
            return await _db.Board.ToListAsync();
        }

        /// <summary>
        /// Hämtar en tavla med ett visst id och alla associerade trådar.
        /// </summary>
        /// <param name="boardId">Tavlans id.</param>
        /// <returns>En tuple med (tavla, trådar) eller null om ingen tavla med det id:t hittades.</returns>
        public async Task<(Board, List<Thread>)?> FetchBoardAndThreads(Guid boardId)
        {
            var board = await _db.Board.FirstAsync(b => b.Id == boardId);
            if (board == null)
                return null;

            var threads = await _db.Thread
                .Where(b => b.BoardId == board.Id)
                .ToListAsync();

            return (board, threads);
        }

    }
}
