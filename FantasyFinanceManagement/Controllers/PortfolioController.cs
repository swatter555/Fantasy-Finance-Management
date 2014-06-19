using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyFinanceManagement.Helpers;
using FantasyFinanceManagement.Models;


namespace FantasyFinanceManagement.Controllers
{
    public class PortfolioController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Portfolio";

            return View();
        }

        [HttpPost]
        public ActionResult Index(PortfolioSubmitModel Model)
        {
            ViewBag.Title = "Portfolio";

            if (ModelState.IsValid)
            {
                StockSymbolData symbolData = new StockSymbolData(Model.Symbol);

                if (symbolData.Last == 0)
                {
                    return View("Error", new ErrorMessage("You wanted a qoute for an invalid symbol."));
                }
                else
                {
                    Session["SymbolData"] = symbolData;
                }

                return RedirectToAction("Index", "Portfolio");
            }

            return View(Model);
        }
    }
}