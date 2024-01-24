using LibraryProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryProject.Controllers
{
    public class SignupController : Controller
    {
        private readonly Library1Context _dbContext;

        public SignupController(Library1Context dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(Users userModel)
        {

                using (var dbContext = new Library1Context())
                {

                    var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == userModel.Username);

                    if (existingUser != null)
                    {
                        ViewBag.ErrorMessage = "Username already taken. Please choose another.";
                        return View(userModel);
                    }

                    dbContext.Users.Add(userModel);
                    dbContext.SaveChanges();

                    ViewBag.SuccessMessage = "Signup successful!";
                }
           

            return RedirectToAction("Login", "Login");
        }

    }
}