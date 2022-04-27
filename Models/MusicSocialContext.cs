using Microsoft.EntityFrameworkCore;

namespace MusicSocial.Models
{
    public class MusicSocialContext : DbContext
    {
        public MusicSocialContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users { get; set; }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumRating> AlbumRatings { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}