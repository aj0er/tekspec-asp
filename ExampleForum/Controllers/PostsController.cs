using ExampleForum.Data;
using ExampleForum.Models.Requests;
using ExampleForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleForum.Controllers
{
    public class PostsController : Controller
    {

        private ExampleForumContext _context;
        private ILogger<PostsController> _logger;
        private readonly Guid _exampleId = Guid.Parse("8c348d2b-9321-41cf-aac7-ce8cd4176c9a");

        public PostsController(ExampleForumContext context, ILogger<PostsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("ThreadId,Content")] CreatePostRequest request)
        {
            var author = await _context.User.FindAsync(_exampleId);
            if (author == null)
                return Unauthorized();

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Author = author,
                Content = request.Content,
                ThreadId = request.ThreadId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };

            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();
            return Redirect($"/Threads/{request.ThreadId}");
        }

        [HttpDelete]
        [Route("{controller}/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = _context.Post.Single(e => e.Id == id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("{controller}/{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Content")] Post post)
        {
            if (id != post.Id)
                return NotFound();

            try
            {
                _context.Attach(post);
                _context.Entry(post)
                    .Property(x => x.Content)
                    .IsModified = true;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
