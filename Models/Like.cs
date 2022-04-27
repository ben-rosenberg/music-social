using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSocial.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation and foreign keys
        public int PostId { get; set; }
        public Post LikePost { get; set; }

        public int UserId { get; set; }
        public User LikeUser { get; set; }
    }
}