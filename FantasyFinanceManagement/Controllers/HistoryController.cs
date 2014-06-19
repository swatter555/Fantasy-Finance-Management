using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasyFinanceManagement.Controllers
{
    public class HistoryController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Transaction History";

            return View();
        }
    }
}