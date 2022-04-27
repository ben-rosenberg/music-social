using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using MusicSocial.Models;

namespace MusicSocial.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(ILogger<HomeController> logger, MusicSocialContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (_IsLoggedIn)
            {
                return RedirectToAction("Dashboard", "Post");
            }

            return View("Index");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) { return View("Index"); }

            User dbUser = _db.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", "Incorrect email or password");
                return View("Index");
            }

            PasswordHasher<LoginUser> passwordHasher = new PasswordHasher<LoginUser>();
            PasswordVerificationResult isPasswordMatch = passwordHasher.VerifyHashedPassword(
                loginUser, dbUser.Password, loginUser.LoginPassword);

            if (isPasswordMatch == 0)
            {
                ModelState.AddModelError("LoginPassword", "Incorrect email or password");
                return View("Index");
            }

            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            return RedirectToAction("Dashboard", "Post");
        }

        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if (!ModelState.IsValid) { return View("Index"); }

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            newUser.Password = passwordHasher.HashPassword(newUser, newUser.Password);

            _db.Users.Add(newUser);
            _db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Dashboard", "Post");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Utility attributes
        private readonly ILogger<HomeController> _logger;
        private MusicSocialContext _db;

        private int? _UserId { get => HttpContext.Session.GetInt32("UserId"); }
        private bool _IsLoggedIn { get => _UserId != null; }
    }
}
