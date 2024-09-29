using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Models
{
    public class SellerAuthorizationAttribute : AuthorizeAttribute
    {
        // Encode and decode methods for Base64 URL safe strings
        public string EncodeToBase64UrlSafe(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            string base64 = System.Convert.ToBase64String(plainTextBytes);
            return base64.Replace("+", "-").Replace("/", "_").TrimEnd('=');
        }

        public string DecodeFromBase64UrlSafe(string base64UrlSafe)
        {
            try
            {
                string base64 = base64UrlSafe.Replace("-", "+").Replace("_", "/");
                switch (base64.Length % 4)
                {
                    case 2: base64 += "=="; break;
                    case 3: base64 += "="; break;
                }
                var base64EncodedBytes = System.Convert.FromBase64String(base64);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (FormatException)
            {
                return "Invalid Base64 string";
            }
        }

        // Core authorization logic
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Check if the user is authenticated
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            // Check if the seller role cookie is present
            string encodedCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerValue = EncodeToBase64UrlSafe("Sellers");

            // Retrieve the seller role cookie
            var sellerRoleCookie = httpContext.Request.Cookies[encodedCookieName];

            // Validate the cookie value
            return sellerRoleCookie != null && sellerRoleCookie.Value == encodedSellerValue;
        }

        // Handle unauthorized access
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string ReturnURL = filterContext.HttpContext.Request.Url.AbsoluteUri;

                // Redirect unauthenticated users to the login page
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Home", action = "SellerLogin" , returnURL = ReturnURL}
                    )
                );
            
           
        }
    }

}