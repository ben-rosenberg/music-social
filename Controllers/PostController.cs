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

            newPost.UserId = (int)_UserId;
            _db.Posts.Add(newPost);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
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