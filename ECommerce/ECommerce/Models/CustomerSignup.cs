using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class CustomerSignup
    {
        [Required(ErrorMessage = "FullName is required")]
        [StringLength(maximumLength: 30, MinimumLength = 4, ErrorMessage = "max length is 20 and min length is 4")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Enter valid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|" +
                 "(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
                  ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), " +
                  "lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        //[StringLength(100, ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 " +
        //    "of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)", MinimumLength = 8)]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string CreatedDate { get; set; }


        public string CustomerToken { get; set; }
        public string CustomerID { get; set; }


    }
}