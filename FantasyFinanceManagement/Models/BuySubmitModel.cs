using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyFinanceManagement.Models
{
    public class BuySubmitModel
    {
        [Required(ErrorMessage = "Please enter a valid stock symbol")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Please enter a valid stock symbol")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "Please enter the number of shares you want to buy")]
        public int Shares { get; set; }
    }

    public class TransactionData
    {
        public System.Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string Time { get; set; }
        public System.Guid TransId { get; set; }
    }
}