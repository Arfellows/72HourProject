using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using _72HourProject.Models;
using System.Data.Entity;

namespace _72HourProject.Controllers
{
    public class ReplyController : ApiController
    {
        private readonly ReplyDbContext _context = new ReplyDbContext();
        //POST(Create)
        //api/reply
        [HttpPost]
        public async Task<IHttpActionResult> PostReply([FromBody] Reply model)
        {
            if (ModelState.IsValid)
            {
                //store the model in the database   
                _context.Replies.Add(model);
                int changesCount = await _context.SaveChangesAsync();

                return Ok("Good job your reply was created.");
            }
            //if not, rejection
            return BadRequest(ModelState);
        }
        //GET all
        //API/Reply
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Reply> replies = await _context.Replies.ToListAsync();
            return Ok(replies);
        }


        //GET BY ID
        //api/Reply
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Reply reply = await _context.Replies.FindAsync(id);

            if (reply != null)
            {
                return Ok(reply);
            }
            return NotFound();
        }

        //Put/ update
        //api/Reply/(id)

        [HttpPut]
        public async Task<IHttpActionResult> UpdateReply([FromUri] int id, [FromBody] Reply updatedReply)
        {
        //check the ids if they match
        if(id != updatedReply?.ReplyId )
            {
                return BadRequest("Ids do not match");
            }

            //check model state
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //fine the reply in the database
            Reply reply = await _context.Replies.FindAsync(id);
            if (reply is null)
                return NotFound();

            reply.ReplyText = updatedReply.ReplyText;
            await _context.SaveChangesAsync();
            return Ok("The reply was edited");
        
        }
        //DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteReply([FromUri] int id)
        {
            Reply reply = await _context.Replies.FindAsync(id);

            if (reply is null)
                return NotFound();

            _context.Replies.Remove(reply);

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok("The reply was Deleted");
            }
            return InternalServerError();
        }

   }
}
