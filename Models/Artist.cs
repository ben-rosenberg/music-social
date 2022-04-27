using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace MusicSocial.Models
{
    public class Artist
    {
        // Table columns
        [Key]
        public int ArtistId { get; set; }

        [Required]
        [Display(Name = "Artist Name")]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation property: this artist's albums
        public List<Album> Albums { get; set; }
    }
}