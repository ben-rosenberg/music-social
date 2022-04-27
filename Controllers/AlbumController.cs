using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using MusicSocial.Models;

namespace MusicSocial.Controllers
{
    public class AlbumController : Controller
    {
        public AlbumController(MusicSocialContext db) { _db = db; }

        [HttpGet("albums")]
        public IActionResult All()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            List<Album> allAlbums = _db.Albums
                .Include(album => album.AlbumArtist)
                .OrderByDescending(album => album.ReleaseDate)
                .ToList();

            return View("All", allAlbums);
        }

        [HttpGet("albums/{albumId}")]
        public IActionResult Details(int albumId)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            Album dbAlbum = _db.Albums
                .Include(album => album.AlbumArtist)
                .Include(album => album.Posts)
                .FirstOrDefault(album => album.AlbumId == albumId);
            
            return View("Details", dbAlbum);
        }

        [HttpGet("albums/new")]
        public IActionResult New()
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }

            ViewBag.AllArtists = _db.Artists
                .OrderBy(artist => artist.Name)
                .ToList();

            return View("New");
        }

        [HttpPost("albums/create")]
        public IActionResult Create(Album newAlbum)
        {
            if (!_IsLoggedIn) { return RedirectToAction("Index", "Home"); }
            if (!ModelState.IsValid)
            {
                ViewBag.AllArtists = _db.Artists
                    .OrderBy(artist => artist.Name)
                    .ToList();
                return View("New");
            }

            Artist dbArtistForThisAlbum = _db.Artists
                .FirstOrDefault(artist => artist.ArtistId == newAlbum.ArtistId);
            
            newAlbum.AlbumArtist = dbArtistForThisAlbum;
            dbArtistForThisAlbum.Albums = new List<Album>();
            dbArtistForThisAlbum.Albums.Add(newAlbum);

            _db.Albums.Add(newAlbum);
            _db.SaveChanges();

            return RedirectToAction("Details", new { albumId = newAlbum.AlbumId });
        }

        // Database
        private MusicSocialContext _db;

        // Utility attributes
        private int? _UserId { get => HttpContext.Session.GetInt32("UserId"); }
        private bool _IsLoggedIn { get => _UserId != null; }
    }
}