using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyFinanceManagement.Models
{
    public class DepositSubmitModel
    {
        [Required(ErrorMessage = "Please enter the deposit amount")]
        [DataType(DataType.Currency)]
        public string DepositAmt { get; set; }
    }
}