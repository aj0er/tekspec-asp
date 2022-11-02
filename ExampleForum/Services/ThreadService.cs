using ExampleForum.Areas.Identity.Data;
using ExampleForum.Data;
using ExampleForum.Models;
using ExampleForum.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Thread = ExampleForum.Models.Thread;

namespace ExampleForum.Services
{
    /// <summary>
    /// Tjänst som hanterar CRUD för (<see cref="Thread"/>).
    /// </summary>
    public class ThreadService
    {

        private readonly ExampleForumContext _db;

        public ThreadService(ExampleForumContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Hämtar en tråd med ett visst id och dess inlägg.
        /// </summary>
        /// <param name="threadId">Trådens id.</param>
        /// <returns>En tuple med (tråd, inlägg) eller null om ingen tråd med id:t fanns.</returns>
        public async Task<(Thread, List<Post>)?> FetchThreadAndPosts(Guid threadId)
        {
            var thread = await _db.Thread.FirstAsync(b => b.Id == threadId);
            if (thread == null)
                return null;

            var posts = await _db.Post
                .Where(b => b.ThreadId == thread.Id)
                .Include(p => p.Author)
                .OrderBy(p => p.Created)
                .ToListAsync();

            return (thread, posts);
        }

        /// <summary>
        /// Försöker ta bort en tråd från databasen.
        /// </summary>
        /// <param name="threadId">Trådens ID.</param>
        /// <param name="user">Användaren som försöker ta bort tråden.</param>
        /// <returns></returns>
        public async Task<bool> DeleteThread(Guid threadId, ExampleForumUser user)
        {
            var thread = await _db.Thread
                           .Where(b => 
                                b.Id == threadId && 
                                b.AuthorId == user.Id) // Användaren får bara ta bort sina egna inlägg.
                           .FirstAsync();

            if (thread == null)
                return false;

            _db.Thread.Remove(thread);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Försöker skapa en ny tråd i en tavla tillsammans med ett första inlägg.
        /// </summary>
        /// <param name="boardId">ID för tavlan att skapa tråden inom.</param>
        /// <param name="request">Förfrågan med b.la. titel och den första inläggets innehåll.</param>
        /// <param name="user">Användaren som försöker skapa tråden.</param>
        /// <returns>Id för den nyskapade tråden eller null om det inte lyckades.</returns>
        public async Task<Guid?> CreateThread(Guid boardId, CreateThreadRequest request, ExampleForumUser user)
        {
            var threadId = Guid.NewGuid();
            var thread = new Thread
            {
                Id = threadId,
                Name = request.Title,
                BoardId = boardId,
                Author = user,
            };

            var post = new Post
            {
                Id = Guid.NewGuid(),
                ThreadId = threadId,
                Author = user,
                Content = request.Content,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            _db.Post.Add(post);
            _db.Add(thread);
            await _db.SaveChangesAsync();
            return threadId;
        }

        /// <summary>
        /// Försöker hämta en tråd baserad på dess id.
        /// </summary>
        /// <param name="threadId">ID för tråden som ska ändras</param>
        /// <returns>Tråden med det specificerade ID:t eller null om den inte fanns.</returns>
        public async Task<Thread?> FetchThreadById(Guid threadId)
        {
            return await _db.Thread.FindAsync(threadId);
        }

    }
}
