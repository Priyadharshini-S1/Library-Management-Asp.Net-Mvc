using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Models;

namespace LibraryProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly Library1Context _dbContext; 

        public LoginController(Library1Context dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (IsValidUser(username, password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),

            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Books"); 
            }
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        //public IActionResult Logout()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); 
        }

        private bool IsValidUser(string username, string password)
        {
           
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user != null;
        }
    }
}
