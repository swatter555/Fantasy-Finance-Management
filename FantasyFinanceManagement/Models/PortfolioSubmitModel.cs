using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace FantasyFinanceManagement.Models
{
    public class PortfolioSubmitModel
    {
        [Required(ErrorMessage = "Please enter a valid stock symbol")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Please enter a valid stock symbol")]
        public string Symbol { get; set; }
    }
}