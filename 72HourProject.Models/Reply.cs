using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72HourProject.Models
{
    public  class Reply
    {
        [Key]
        public int ReplyId { get; set; }
        [Required]
        public string ReplyText { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
    }
}
