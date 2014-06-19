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
    public class SellController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Sell";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.BuySubmitModel Model)
        {
            ViewBag.Title = "Sell";

            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirm", Model);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Confirm(Models.BuySubmitModel Model)
        {
            ViewBag.Title = "Confirmation";

            StockSymbolData symbolData = new StockSymbolData(Model.Symbol);

            if (symbolData.Last == 0)
            {
                return View("Error", new ErrorMessage("You wanted a qoute for an invalid symbol."));
            }

            using (var db = new FantasyFinanceManagement.Models.FantasyFinanceDatabaseEntities())
            {
                HttpCookie cookie = Request.Cookies["0101111001010010110"];
                var email = cookie.Value;
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    // Create a transaction object
                    TransactionData transData = new TransactionData();

                    // Fill it current transaction data
                    transData.Id = user.Id;
                    transData.Symbol = symbolData.Symbol;
                    transData.Type = "SELL";
                    transData.Shares = Model.Shares;
                    transData.Price = symbolData.Last;
                    transData.Total = symbolData.Last * Model.Shares;
                    transData.Time = DateTime.Now.ToString();
                    transData.TransId = System.Guid.NewGuid();

                    // Store the transaction in Session
                    Session["Transaction"] = transData;
                }
                else
                {
                    return View("Error", new ErrorMessage("A database error has occured."));
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Confirm(string button)
        {
            ViewBag.Title = "Confirmation";

            if (button == "confirm")
            {
                try
                {
                    using (var db = new FantasyFinanceManagement.Models.FantasyFinanceDatabaseEntities())
                    {
                        // Get session transaction data
                        var transData = Session["Transaction"] as FantasyFinanceManagement.Models.TransactionData;

                        // Use cookie to get current user based on email
                        HttpCookie cookie = Request.Cookies["0101111001010010110"];
                        var email = cookie.Value;
                        var user = db.Users.FirstOrDefault(u => u.Email == email);

                        // Perform a query on portfolio table to determine if user already owns the stock
                        var pQuery = from portfolio in db.Portfolios
                                     where portfolio.Id == transData.Id && portfolio.Symbol == transData.Symbol
                                     select portfolio;

                        // If the user already owns stock, add number of shares to existing entry
                        if (pQuery.Count() > 0)
                        {
                            var entry = pQuery.FirstOrDefault();

                            if (transData.Shares > entry.Shares)
                            {
                                return View("Error", new ErrorMessage("You don't own that much stock."));
                            }

                            if ((entry.Shares - transData.Shares) < 1)
                            {
                                db.Portfolios.Remove(entry);
                            }
                            else
                            {
                                entry.Shares -= transData.Shares;
                            }
                        }
                        else
                        {
                            return View("Error", new ErrorMessage("You cannot sell a stock you don't own."));
                        }

                        // Create a new transaction record
                        var transaction = db.Transactions.Create();
                        transaction.Id = transData.Id;
                        transaction.Symbol = transData.Symbol;
                        transaction.Price = transData.Price;
                        transaction.Shares = transData.Shares;
                        transaction.Time = transData.Time;
                        transaction.Total = transData.Total;
                        transaction.Trans_Id = transData.TransId;
                        transaction.Type = transData.Type;

                        // Add the transaction
                        db.Transactions.Add(transaction);

                        // Deduct funds from the user account
                        user.Cash += transData.Total;

                        // Save changes to database
                        db.SaveChanges();

                        return View("Success");
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
            }
            else if (button == "cancel")
            {
                return RedirectToAction("Index", "Portfolio");
            }

            return View("Error", new ErrorMessage("An error occured during purchase. Purchase did not occur."));
        }
    }
}