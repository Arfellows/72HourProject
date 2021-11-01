using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _72HourProject.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        public string Comments { get; set; }

        //public virtual List<Comment> {get; set;}

    }
}