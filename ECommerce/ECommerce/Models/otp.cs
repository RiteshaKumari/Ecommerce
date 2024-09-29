using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class otp
    {
        [Required(ErrorMessage = "required.")]
        public int OTP { get; set; }
    }
}