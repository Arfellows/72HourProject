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
    public class CommentController : ApiController
    { 
        private readonly CommentDbContext _context = new CommentDbContext();
        // POST (create)
        //api/Comment
        [HttpPost]
        public async Task<IHttpActionResult> CreateComment([FromBody] Comment model) 
        {
            if(model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            if (ModelState.IsValid)
            {
                _context.Comments.Add(model);
                int changeCount = await _context.SaveChangesAsync();
                return Ok("Comment was created.");
            }
            return BadRequest(ModelState);
        }
        //GET ALL
        //api/Comment
        public async Task<IHttpActionResult> GetAll()
        {
            List<Comment> comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }
        //Get By ID
        //api/Comment/{id}
        public async Task<IHttpActionResult> GetById([FromBody] int id)
        {
            Comment comment = await _context.Comments.FindAsync(id);
            if(comment != null)
            {
                return Ok(comment);
            }
            return NotFound();
        }
        //PUT (update)
        //api/Comment/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateComment([FromBody] int id, Comment updatedComment)
        {
            //Check the ids for match
            if(id != updatedComment.Id)
            {
                return BadRequest("Ids do not match");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Find the comment in the db
            Comment comment = await _context.Comments.FindAsync(id);
            if(comment is null)
            {
                return NotFound();
            }
            //Update the props
            comment.Text = updatedComment.Text;
            //Save the changes
            await _context.SaveChangesAsync();
            return Ok("The comment was updated.");
        }
        //Delete (delete)
        //api/Comment/{id}
        public async Task<IHttpActionResult> DeleteComment([FromBody] int id)
        {
            Comment comment = await _context.Comments.FindAsync(id);

            if (comment is null)
                return NotFound();

            _context.Comments.Remove(comment);

            if (await _context.SaveChangesAsync() == 1)
                return Ok("The comment was deleted.");

            return InternalServerError();
        }
    }
}
