using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyFinanceManagement.Helpers
{
    public class ErrorMessage
    {
        /*
         * Used to display the error page.
         * Usage: return View("Error", new ErrorMessage("Description"));
         */
        string ErrorString;

        public ErrorMessage(string Message)
        {
            ErrorString = Message;
        }

        public string GetError()
        {
            return ErrorString;
        }
    }
}