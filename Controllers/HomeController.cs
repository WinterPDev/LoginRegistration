using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetString("User") == null)
            {
                return View("Index");
            }
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost("registerUser")]
        public IActionResult Register(User user)
        {
            // Check initial ModelState
            if(ModelState.IsValid)
            {
                // If a User exists with provided email
                if(_context.Users.Any(u => u.Email == user.Email))
                {
                    // Manually add a ModelState error to the Email field, with provided
                    // error message
                    ModelState.AddModelError("Email", "Email already in use!");
                    
                    return View("Index");
                } 
            }

                if(ModelState.IsValid)
                {
                    
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    _context.Add(user);
                    _context.SaveChanges();
                    HttpContext.Session.SetString("User", user.Email);
                    return RedirectToAction("Success");
                }
                else
                {
                    return View("Index");
                }
        }


        [HttpPost("loginUser")]
        public IActionResult Login(LoginUser userSubmission)
            {
                if(ModelState.IsValid)
                {
                    // If inital ModelState is valid, query for a user with provided email
                    var userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                    // If no user exists with provided email
                    if(userInDb == null)
                    {
                        // Add an error to ModelState and return to View!
                        ModelState.AddModelError("Email", "Invalid Email/Password");
                        return View("Login");
                    }
                    
                    // Initialize hasher object
                    var hasher = new PasswordHasher<LoginUser>();
                    // verify provided password against hash stored in db
                    var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
                    
                    // result can be compared to 0 for failure
                    if(result == 0)
                    {
                        ModelState.AddModelError("Email", "Invalid Email/Password");
                        return View("Login");
                    }
                    HttpContext.Session.SetString("User", userInDb.Email);
                    return View("Success");
                }
                return View("Login");
            }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    }

}
