using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using MusicSocial.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicSocial.Controllers
{
    public class PostController : Controller
    {
        public PostController(MusicSocialContext db) { _db = db; }
        
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            List<Post> allPosts = _db.Posts
                .Include(p => p.PostUser)
                .Include(p => p.PostAlbum).ThenInclude(a => a.AlbumArtist)
                .Include(post => post.Likes)
                .Include(post => post.Comments)
                .OrderByDescending(post => post.CreatedAt)
                .ToList();

            return View("Dashboard", allPosts);
        }

        [HttpGet("posts/new")]
        public IActionResult NewPost()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
            
            List<Album> allAlbums = _db.Albums.ToList();
            ViewBag.AllAlbums = allAlbums;

            return View("NewPost");
        }

        [HttpPost("posts/create")]
        public IActionResult CreatePost(Post newPost)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            if (!ModelState.IsValid) 
            { 
                List<Album> allAlbums = _db.Albums.ToList();
                ViewBag.AllAlbums = allAlbums;
                return View("NewPost");
            }

            User dbUser = _db.Users
                .Include(user => user.Posts)
                .FirstOrDefault(user => user.UserId == _UserId);

            newPost.UserId = (int)_UserId;
            newPost.PostUser = dbUser;

            Album dbAlbum = _db.Albums
                .Include(album => album.AlbumRatings)
                .Include(album => album.Posts)
                .FirstOrDefault(album => album.AlbumId == newPost.AlbumId);

            if (dbAlbum == null) { return RedirectToAction("Dashboard"); }

            AlbumRating newAlbumRating = new AlbumRating()
            {
                Rating = newPost.AlbumRatingNumber,
                AlbumId = newPost.AlbumId,
                RatingAlbum = dbAlbum,
                PostId = newPost.PostId,
                RatingPost = newPost
            };

            newPost.PostRating = newAlbumRating;
            newPost.RatingId = newAlbumRating.AlbumRatingId;
            
            dbAlbum.AlbumRatings.Add(newAlbumRating);
            dbAlbum.Posts.Add(newPost);
            dbUser.Posts.Add(newPost);

            _db.Posts.Add(newPost);
            _db.AlbumRatings.Add(newAlbumRating);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost("posts/{postId}/like")]
        public IActionResult Like(int postId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            User dbUser = _db.Users
                .Include(user => user.Likes)
                .FirstOrDefault(user => user.UserId == _UserId);
            Post dbPost = _db.Posts
                .Include(post => post.Likes)
                .FirstOrDefault(post => post.PostId == postId);
            Like dbLike = _db.Likes
                .FirstOrDefault(like => like.PostId == postId && like.UserId == _UserId);

            // User hasn't liked this post yet
            if (dbLike == null)
            {
                Like newLike = new Like()
                {
                    LikeUser = dbUser,
                    LikePost = dbPost,
                    UserId = (int)_UserId,
                    PostId = postId
                };

                dbPost.Likes.Add(newLike);
                dbUser.Likes.Add(newLike);

                _db.Likes.Add(newLike);
            }

            // User has already liked this post and is unliking it
            else
            {
                dbPost.Likes.Remove(dbLike);
                dbUser.Likes.Remove(dbLike);
                _db.Likes.Remove(dbLike);
            }

            _db.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost("likes/{postId}/comment")]
        public IActionResult Comment(int postId, Comment newComment)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            User dbUser = _db.Users
                .FirstOrDefault(user => user.UserId == _UserId);
            Post dbPost = _db.Posts
                .FirstOrDefault(post => post.PostId == postId);

            newComment.PostId = postId;
            newComment.CommentPost = dbPost;
            newComment.UserId = (int)_UserId;
            newComment.CommentUser = dbUser;

            if (dbPost.Comments == null) { dbPost.Comments = new List<Comment>(); }
            if (dbUser.Comments == null) { dbUser.Comments = new List<Comment>(); }
            dbUser.Comments.Add(newComment);
            dbPost.Comments.Add(newComment);

            _db.Comments.Add(newComment);
            _db.SaveChanges();

            return RedirectToAction("ViewComments", new { postId = postId });
        }

        [HttpGet("posts/{postId}")]
        public IActionResult ViewComments(int postId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            Post dbPost = _db.Posts
                .Include(post => post.Likes)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.CommentUser)
                .Include(post => post.PostUser)
                .Include(post => post.PostAlbum)
                    .ThenInclude(album => album.AlbumArtist)
                .FirstOrDefault(post => post.PostId == postId);
            
            return View("ViewComments", dbPost);
        }

        /* [HttpGet("posts/{postId}")]
        public IActionResult Details(int postId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
        }

        // If we want to be able to create a new post only directly from the
        // dashboard like Facebook, this route is unnecessary. Having a
        // separate page is probably simpler though.

        [HttpGet("posts/{postId}/edit")]
        public IActionResult Edit(int postId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
        }

        [HttpPost("posts/{postId}/update")]
        public IActionResult Update(int postId, Post updatedPost)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
            if (!ModelState.IsValid) { return View("Edit"); }
        }

        [HttpGet("posts/{postId}/delete")]
        public IActionResult Delete(int postId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
        } */

        // Database
        private MusicSocialContext _db;
        
        // Utility attributes
        private int? _UserId { get => HttpContext.Session.GetInt32("UserId"); }
        private bool _IsLoggedIn { get => _UserId != null; }
    }
}