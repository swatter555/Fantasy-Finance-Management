using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyFinanceManagement.Models;
using FantasyFinanceManagement.Helpers;

namespace FantasyFinanceManagement.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Login";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.LoginSubmitModel Model)
        {
            ViewBag.Title = "Login";

            if (ModelState.IsValid)
            {
                if (ValidateUserData(Model.Email,Model.Password))
                {
                    // Create the cookie
                    HttpCookie cookie = new HttpCookie("0101111001010010110");
                    
                    // Save email in the cookie
                    cookie.Value = Model.Email;
                    
                    // Set to expire in an hour
                    cookie.Expires = DateTime.Now.AddMinutes(60d);
                
                    // Add to httpResponse
                    Response.Cookies.Add(cookie);

                    // Redirect to user portfolio
                    return RedirectToAction("Index", "Portfolio");
                }

                // User doesn't exist, present error page
                return View("Error", new ErrorMessage("User doesn't exist, check login information."));  
            }

            return View(Model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            ViewBag.Title = "Logout";

            if (Request.Cookies["0101111001010010110"] != null)
            {
                HttpCookie cookie = new HttpCookie("0101111001010010110");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index","LogIn");
        }

        private bool ValidateUserData(string email, string password)
        {
            // Get crypto service
            var cryptoService = new SimpleCrypto.PBKDF2();

            // Get database entity
            using (var db = new FantasyFinanceDatabaseEntities())
            {
                // Get user that matched email
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    // Hash the password given with user salt
                    string hashedPassword = cryptoService.Compute(password, user.Salt);

                    // Compare the result with the user hash, they should match if correct
                    // password is provided
                    if (cryptoService.Compare(user.Hash, hashedPassword))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}