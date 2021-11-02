using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Models
{
    public class ReplyDbContext : DbContext
    {
        public ReplyDbContext() : base("ReplyConnection")
        {

        }
        public DbSet<Reply> Replies { get; set; }
    }
}
