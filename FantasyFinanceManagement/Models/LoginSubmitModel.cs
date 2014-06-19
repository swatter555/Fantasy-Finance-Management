using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FantasyFinanceManagement.Models
{
    public class LoginSubmitModel
    {
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password between 8 and 20 characters")]
        [StringLength(20,MinimumLength=8, ErrorMessage = "Passwords must be between 8 and 20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}