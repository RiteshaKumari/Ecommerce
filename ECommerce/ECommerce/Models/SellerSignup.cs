using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class SellerSignup
    {
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "* Enter Correct Email")]
        [Required(ErrorMessage = "This Field is Neccesary")]
        
        public string Email { get; set; }
        public string OTP { get; set; }
        [Required(ErrorMessage ="* Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
       
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string IDProofType { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string IDProof { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string SellerAddress { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string SellerState { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string SellerCountry { get; set; }
        [Required(ErrorMessage = "* Required")]
        //[StringLength(6, MinimumLength = 6)]
        public string SellerPincode { get; set; }
        [Required(ErrorMessage = "* Required")]
    
        public string BankName { get; set; }
        [Required(ErrorMessage = "* Required")]
        //[StringLength(11, MinimumLength = 11)]
        public string IFSECode { get; set; }
        [Required(ErrorMessage = "* Required")]
        //[StringLength(16, MinimumLength = 16)]
        public string AccountNo { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string CompanyAddress { get; set; }
        [Required(ErrorMessage = "* Required")]
        //[StringLength(15, MinimumLength = 15)]
        public string CompanyGST { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string CompanyEmail { get; set; }
        [Required(ErrorMessage = "* Required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        
        public string CompanyPhone { get; set; }


    }
}