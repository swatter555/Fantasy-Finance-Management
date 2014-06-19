using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyFinanceManagement.Helpers;

namespace FantasyFinanceManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Fantasy Finance";

            /* Use a cookie so user need not log in more than once per hour */

            // Attempt to get cookie
            HttpCookie cookie = Request.Cookies.Get("0101111001010010110");

            // Check if cookie present
            if (cookie != null)
            {
                // Put cookie in ViewBag
                ViewBag.Cookie = cookie;

                // Go directly to portfolio
                return RedirectToAction("Index", "Portfolio");
            }

            return RedirectToAction("Index", "LogIn");            
        }
    }
}