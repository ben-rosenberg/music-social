using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace MusicSocial.Models
{
    public class Comment
    {
        // Table columns
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Comment must not be empty")]
        [Display(Name = "Comment")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation and foreign key properties.
        public int PostId { get; set; }
        public Post CommentPost { get; set; }

        public int UserId { get; set; }
        public User CommentUser { get; set; }
    }
}