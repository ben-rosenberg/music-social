using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MusicSocial.Models;

namespace MusicSocial.Controllers
{
    public class ArtistController : Controller
    {
        public ArtistController(MusicSocialContext db) { _db = db; }
        
        [HttpGet("artists/{artistId}")]
        public IActionResult Details(int artistId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            Artist dbArtist = _db.Artists
                .FirstOrDefault(artist => artist.ArtistId == artistId);
            
            return View("Details", dbArtist);
        }

        [HttpGet("artists/new")]
        public IActionResult New()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            return View("New");
        }

        [HttpPost("artists/create")]
        public IActionResult Create(Artist newArtist)
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