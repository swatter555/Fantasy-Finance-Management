using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyFinanceManagement.Models;
using FantasyFinanceManagement.Helpers;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace FantasyFinanceManagement.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Register";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.RegisterSubmitDataModel Model)
        {
            ViewBag.Title = "Register";

            if (ModelState.IsValid)
            {
                if (Model.Hash == Model.Salt)
                {
                    try
                    {
                        using (var db = new FantasyFinanceDatabaseEntities())
                        {
                            // Create the password hash and salt
                            var cryptoService = new SimpleCrypto.PBKDF2();
                            var salt = cryptoService.GenerateSalt();
                            var hash = cryptoService.Compute(Model.Hash);

                            // Create a user
                            var user = db.Users.Create();

                            // Fill in registration information
                            user.FirstName = Model.FirstName;
                            user.LastName = Model.LastName;
                            user.Email = Model.Email;
                            user.Salt = salt;
                            user.Hash = hash;
                            user.Id = Guid.NewGuid();

                            // Add user to database
                            db.Users.Add(user);
                            db.SaveChanges();

                            return RedirectToAction("SuccessfulRegistration", "Register");
                        } 
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }

                    return View("Error", new ErrorMessage("There was an error adding user to database."));

                }
                else
                {
                    return View("Error", new ErrorMessage("The passwords you entered did not match, please try again."));
                }
            }

            return View(Model);
        }

        [HttpGet]
        public ActionResult SuccessfulRegistration()
        {
            ViewBag.Title = "Succeessful Registration";

            return View();
        }
    }
}