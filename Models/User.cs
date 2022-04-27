using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace MusicSocial.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Username must be at least 2 characters")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [NewEmail]
        public string Email { get; set; }

        [Required]
        [Password]
        [MinLength(8, ErrorMessage = "Password must be longer than 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation and foreign key properties...
        public List<Post> Posts { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Rsvps { get; set; }
    }

    public class NewEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            MusicSocialContext db = (MusicSocialContext)validationContext.GetService(typeof(MusicSocialContext));

            User user = db.Users.FirstOrDefault(u => u.Email == (string)value);
            
            if (user != null)
            {
                return new ValidationResult("Email already taken");
            }

            return ValidationResult.Success;
        }
    }

    public class NewUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            MusicSocialContext db = (MusicSocialContext)validationContext.GetService(typeof(MusicSocialContext));
            bool isUsernameTaken = db.Users.Any(user => user.Username == (string)value);

            if (isUsernameTaken)
            {
                return new ValidationResult("Username already taken");
            }

            return ValidationResult.Success;
        }
    }

    public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                Regex number = new Regex(@"[0-9]+");
                Regex uppercaseLetter = new Regex(@"[A-Z]+");
                Regex specialChar = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                if (number.IsMatch((string)value) && uppercaseLetter.IsMatch((string)value) && specialChar.IsMatch((string)value))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Password must contain at least 1 uppercase letter, 1 number, and 1 special character");
        }
    }
}