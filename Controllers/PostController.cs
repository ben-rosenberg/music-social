using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using MusicSocial.Models;

namespace MusicSocial.Controllers
{
    public class PostController : Controller
    {
        public PostController(MusicSocialContext db) { _db = db; }
        
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            return View("Dashboard");
        }

        /* [HttpGet("posts/{postId}")]
        public IActionResult Details(int postId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
        }

        // If we want to be able to create a new post only directly from the
        // dashboard like Facebook, this route is unnecessary. Having a
        // separate page is probably simpler though.
        [HttpGet("posts/new")]
        public IActionResult New()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
            return View("New");
        }

        [HttpPost("posts/create")]
        public IActionResult Create(Post newPost)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
            if (!ModelState.IsValid) { return View("New"); }
        }

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