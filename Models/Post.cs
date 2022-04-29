using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MusicSocial.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [Display(Name = "Review")]
        public string Content { get; set; }

        [Required]
        public int AlbumRatingNumber {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation and foreign keys
        public int UserId { get; set; }
        public User PostUser { get; set; }

        public int AlbumId { get; set; }
        public Album PostAlbum { get; set; }

        public int RatingId { get; set; }
        public AlbumRating PostRating { get; set; }

        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}