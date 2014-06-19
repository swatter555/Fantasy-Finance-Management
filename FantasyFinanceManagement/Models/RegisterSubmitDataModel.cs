using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasyFinanceManagement.Models
{
    public class RegisterSubmitDataModel
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password between 8 and 20 characters")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Passwords must be between 8 and 20 characters")]
        public string Hash { get; set; }

        [Required(ErrorMessage = "Please re-enter your password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Passwords must be between 8 and 20 characters")]
        public string Salt { get; set; }

        [DataType(DataType.Currency)]
        public decimal Cash { get; set; }
    }
}