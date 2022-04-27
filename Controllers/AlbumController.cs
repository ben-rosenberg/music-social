using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using MusicSocial.Models;

namespace MusicSocial.Controllers
{
    public class AlbumController : Controller
    {
        public AlbumController(MusicSocialContext db) { _db = db; }

        [HttpGet("albums/{albumId}")]
        public IActionResult Details(int albumId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            Album dbAlbum = _db.Albums
                .FirstOrDefault(album => album.AlbumId == albumId);
            
            return View("Details", dbAlbum);
        }

        [HttpGet("albums/new")]
        public IActionResult New()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            return View("New");
        }

        [HttpPost("albums/create")]
        public IActionResult Create(Album newAlbum)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
            if (!ModelState.IsValid) { return View("New"); }
        }

        // Database
        private MusicSocialContext _db;

        // Utility attributes
        private int? _UserId { get => HttpContext.Session.GetInt32("UserId"); }
        private bool _IsLoggedIn { get => _UserId != null; }
    }
}