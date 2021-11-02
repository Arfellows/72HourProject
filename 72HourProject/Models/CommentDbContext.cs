using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _72HourProject.Models
{
    public class CommentDbContext : DbContext
    {
        public CommentDbContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Comment> Comments { get; set; }
    }
}