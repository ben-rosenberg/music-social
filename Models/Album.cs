using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSocial.Models
{
    public class Album
    {
        // Database columns
        [Key]
        public int AlbumId { get; set; }

        [Required]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Album Name")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Display properties, i.e. so that the ReleaseDate displays as a short
        // date, NOT a date and time. When displaying ReleaseDate in the view,
        // be sure to either use this property or ReleaseDate.ToShortDateString(),
        // otherwise a defaulted time will be displayed as well.
        [NotMapped]
        public string DisplayReleaseDate { get => ReleaseDate.ToShortDateString(); }
        
        [NotMapped]
        public float AverageRating
        {
            get
            {
                float totalRating = 0.0f;
                foreach (AlbumRating albumRating in AlbumRatings) { totalRating += albumRating.Rating; }
                return totalRating / AlbumRatings.Count();
            }
        }

        // Navigation and foreign key properties
        public int ArtistId { get; set; }
        public Artist AlbumArtist { get; set; }

        public List<Post> Posts { get; set; }
        public List<AlbumRating> AlbumRatings { get; set; }
    }
}