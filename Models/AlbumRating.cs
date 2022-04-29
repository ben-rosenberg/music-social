using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MusicSocial.Models
{
    public class AlbumRating
    {
        // Table columns
        [Key]
        public int AlbumRatingId { get; set; }

        [Required]
        [Range(1, 5)]
        [DecimalInterval(0.5)]
        public float Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation and foreign key properties
        public int AlbumId { get; set; }
        public Album RatingAlbum { get; set; }

        public int PostId { get; set; }
        public Post RatingPost { get; set; }
    }

    // A validation attribute for ensuring that the decimal portion of a
    // floating point number is evenly divisible by the specified decimal
    // interval, or 0.1 by default. In the case of an album rating field with
    // this attribute specified to 0.5, it will ensure that ratings like 2.5
    // and 4 are allowed, but not something like 3.6.
    public class DecimalIntervalAttribute : ValidationAttribute
    {
        public DecimalIntervalAttribute() : base() { _DecimalInterval = 0.1; }

        public DecimalIntervalAttribute(double decimalInterval)
            : base() { _DecimalInterval = decimalInterval; }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            bool isDecimalEvenlyDivisbleByInterval = ((double)value % _DecimalInterval) == 0;

            if (!isDecimalEvenlyDivisbleByInterval)
            {
                return new ValidationResult("Only decimal values in intervals of " + _DecimalInterval.ToString() + "are allowed");
            }

            return ValidationResult.Success;
        }

        private double _DecimalInterval;
    }
}