using _72HourProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace _72HourProject.Controllers
{
    public class PostController : ApiController
    {
        private readonly PostDbContext _context = new PostDbContext();
        //POST - create
        //api/Post
        [HttpPost]
        public async Task<IHttpActionResult> CreatePost([FromBody]Post model)
        {
            if(ModelState.IsValid)
            {
                //store in database
                _context.Posts.Add(model);
                await _context.SaveChangesAsync();
                return Ok("Your post was successfully created!");
            }
            //reject post
            return BadRequest(ModelState);
        }

        //GET ALL
        //api/Post
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Post> posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        //GET BY AUTHOR ID
        //api/Post{AuthorId}
        [HttpGet]
        public async Task<IHttpActionResult> GetByAuthorId([FromUri] Guid id)
        {
            Post post = await _context.Posts.FindAsync(id);
            if(post != null)
            {
                return Ok(post);
            }
            return NotFound();
        }

        //PUT - update
        //api/Post/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePost([FromUri] int id, [FromBody] Post updatedPost)
        {
            //check if IDs match
            if(id != updatedPost?.Id)
            {
                return BadRequest("The IDs do not match.");
            }

            //check ModelState
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //find post in database
            Post post = await _context.Posts.FindAsync(id);

            //if post doesn't exist...
            if (post is null)
                return NotFound();

            //update properties
            post.Title = updatedPost.Title;
            post.AuthorId = updatedPost.AuthorId;

            //save changes
            await _context.SaveChangesAsync();
            return Ok("Your post was updated!");
        }

        //DELETE
        //api/Post/{id}
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePost([FromUri] int id)
        {
            Post post = await _context.Posts.FindAsync(id);

            if (post is null)
                return NotFound();

            _context.Posts.Remove(post);

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok("Your post was deleted!");
            }
            return InternalServerError();
        }
    }
}
