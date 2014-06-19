using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyFinanceManagement.Helpers;
using FantasyFinanceManagement.Models;

namespace FantasyFinanceManagement.Controllers
{
    public class DepositController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Deposit";

            return View();
        }

        [HttpPost]
        public ActionResult Index(DepositSubmitModel Model)
        {
            ViewBag.Title = "Portfolio";

            if (ModelState.IsValid)
            {
                // Get the database entity
                var db = new FantasyFinanceManagement.Models.FantasyFinanceDatabaseEntities();

                // Get cookie
                HttpCookie cookie = Request.Cookies["0101111001010010110"];
                var email = cookie.Value;

                // Get user associated with email in cookie
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    // Attempt to add deposit to account
                    decimal amount;
                    if (Decimal.TryParse(Model.DepositAmt, out amount))
                    {
                        //Change cash and deposits
                        user.Cash += amount;
                        user.Deposits += amount;
                        
                        // Save changes to database
                        db.SaveChanges();

                        return View("Success");
                    }
                    else
                    {
                        return View("Error", new ErrorMessage("An invalid number was entered."));
                    }
                }
                else
                {
                    return View("Error", new ErrorMessage("A database error has occured.")); 
                }
            }

            return View(Model);
        }
    }
}