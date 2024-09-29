using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Models
{
    public class CustomerAuthorizationAttribute : AuthorizeAttribute
    {
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

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Check if the user is authenticated (e.g., via FormsAuthentication or Identity)
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            // Check if the customer role cookie is present
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerRole");
            string encodedCustomerValue = EncodeToBase64UrlSafe("Customer");

            // Retrieve the customer role cookie
            var customerRoleCookie = httpContext.Request.Cookies[encodedCookieName];

            // If the customer role cookie is present and matches "Customer", allow access
            return customerRoleCookie != null && customerRoleCookie.Value == encodedCustomerValue;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirect to login page with the return URL
            string returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;

            // Redirect to the CustomerLogin action with the return URL in the query string
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(
                    new { controller = "Home", action = "CustomerLogin", returnUrl = returnUrl }
                )
            );
        }
    }

}