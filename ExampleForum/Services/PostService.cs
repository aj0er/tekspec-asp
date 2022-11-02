using ExampleForum.Areas.Identity.Data;
using ExampleForum.Data;
using ExampleForum.Models;
using ExampleForum.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace ExampleForum.Services
{
    /// <summary>
    /// Tjänst som hanterar CRUD för (<see cref="Post"/>).
    /// </summary>
    public class PostService
    {

        private readonly ExampleForumContext _db;

        public PostService(ExampleForumContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Hämtar en lista på alla inlägg från databasen.
        /// </summary>
        /// <returns>En lista med alla inlägg som finns.</returns>
        public async Task<List<Post>> GetPosts()
        {
            return await _db.Post.ToListAsync();
        }

        /// <summary>
        /// Försöker skapa ett nytt inlägg i databasen.
        /// </summary>
        /// <param name="threadId">ID för tråden som inlägget skapas i</param>
        /// <param name="request">Förfrågan mappad från controller som innehåller nödvändiga data.</param>
        /// <param name="author">Användaren som försöker skapa ett inlägg.</param>
        /// <returns></returns>
        public async Task<bool> CreatePost(Guid threadId, CreatePostRequest request, ExampleForumUser author)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Author = author,
                Content = request.Content,
                ThreadId = threadId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            await _db.Post.AddAsync(post);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Försöker ta bort ett inlägg baserat på dess id.
        /// </summary>
        /// <param name="id">Inläggets id.</param>
        /// <param name="user">Användaren som försöker ta bort inlägget.</param>
        /// <returns></returns>
        public async Task<bool> DeletePost(Guid id, ExampleForumUser user)
        {
            var post = _db.Post.FirstOrDefault(e => e.Id == id && e.AuthorId == user.Id);
            if (post == null) // Inlägget finns inte.
                return false;

            _db.Post.Remove(post);
            await _db.SaveChangesAsync();
            return true; 
        }

        /// <summary>
        /// Hämtar alla inlägg som skapats i en viss tråd.
        /// </summary>
        /// <param name="threadId">ID för tråden som ska sökas i</param>
        /// <returns>En lista med alla inlägg som skapades i den specificerade tråden</returns>
        public async Task<List<Post>> FetchPostsByThread(Guid threadId)
        {
            var posts = await _db.Post
                            .Where(b => b.ThreadId == threadId)
                            .Include(p => p.Author)
                            .OrderBy(p => p.Created)
                            .ToListAsync();

            return posts;
        }

        /// <summary>
        /// Försöker ändra ett inlägg i databasen.
        /// </summary>
        /// <param name="id">ID för inlägget som ändras.</param>
        /// <param name="request">Ändrade data mappad från controllern.</param>
        /// <param name="user">Användaren som försöker ta bort inlägget.</param>
        /// <returns></returns>
        public async Task<bool> EditPost(Guid id, EditPostRequest request, ExampleForumUser user)
        {
            // Det verkar inte gå att uppdatera ett inlägg med en WHERE-sats, därför måste inlägget hämtas först för att kunna verifiera ägaren.
            var previous = _db.Post.FirstOrDefault(e => e.Id == id);
            if (previous == null)
                return false;

            if (previous.AuthorId != user.Id) // Användaren ska inte kunna ändra andras inlägg
                return false;

            previous.Content = request.Content;

            try
            {
                _db.Entry(previous)
                    .Property(x => x.Content)
                    .IsModified = true;

                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

    }
}
