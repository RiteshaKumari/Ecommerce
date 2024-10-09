using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web.Helpers;
using System.Security.Policy;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web.Routing;
using System.Web.UI;
using Antlr.Runtime;
using System.Reflection;
using ECommerce.Models;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private Utility ul = new Utility();

        //----------------------------------JSONS-----------------------------------------------
        public JsonResult GetStates(string countryId)
        {
            List<SelectCountryState> Res = new List<SelectCountryState>();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@country_id",countryId)
            };

            DataSet DS = ul.fn_DataSet("SelectState", param);
            var Product = DS.Tables[0];
            var Res1 = Product.AsEnumerable().Select(s => new SelectCountryState
            {
                State_name = s.Field<string>("State_name"),

            }).ToList();

            return Json(Res1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSellingCategory(string BusinessId)
        {
            List<SelectProductCategory> Res = new List<SelectProductCategory>();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@BID",BusinessId)
            };

            DataSet DS = ul.fn_DataSet("SelectSellingCategory", param);
            var Product = DS.Tables[0];
            var Res1 = Product.AsEnumerable().Select(s => new SelectProductCategory
            {
                SellingCategory = s.Field<string>("SellingCategory")

            }).ToList();

            return Json(Res1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductCategory(string SellingId)
        {
            List<SelectProductCategory> Res = new List<SelectProductCategory>();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@SID",SellingId)
            };

            DataSet DS = ul.fn_DataSet("SelectProductCategory", param);
            var Product = DS.Tables[0];
            var Res1 = Product.AsEnumerable().Select(s => new SelectProductCategory
            {
                SubCategory = s.Field<string>("SubCategory")

            }).ToList();

            return Json(Res1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSubInventory(string size, string color, string capacity, string piece, string price, string InventoryID, string ProductID)
        {
            List<AddSubInventory> Res = new List<AddSubInventory>();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@size",size),
                   new SqlParameter("@color",color),
                   new SqlParameter("@capacity",capacity),
                   new SqlParameter("@piece",piece),
                   new SqlParameter("@price",price),
                   new SqlParameter("@InventoryID",InventoryID),
                   new SqlParameter("@ProductID",ProductID)
            };

            var isValid = (int)ul.func_ExecuteScalar("AddSubInventory", param);
            if (isValid > 0)
            {
                return Json(isValid, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult DeleteSubInventory(string PSIID)
        {
            List<AddSubInventory> Res = new List<AddSubInventory>();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@PSIID",PSIID)
            };

            var isValid = (int)ul.func_ExecuteScalar("DeleteSubInventory", param);
            if (isValid > 0)
            {
                return Json(isValid, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult DeleteProductImage(string ID)
        {
            List<AddSubInventory> Res = new List<AddSubInventory>();
            SqlParameter[] param = new SqlParameter[]
            {
                   new SqlParameter("@ID",ID)
            };

            var isValid = (int)ul.func_ExecuteScalar("DeleteProductImage", param);
            if (isValid > 0)
            {
                return Json(isValid, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetPDetails(string pid)
        {
            // Parameters for the stored procedure
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@pid", pid)
            };

            DataSet DS = ul.fn_DataSet("OfferPDetails", param);
            var Product = DS.Tables[0];
            var Res1 = Product.AsEnumerable().Select(s => new RunOffer
            {
                PDetails = s.Field<string>("PDetails")

            }).ToList();

            return Json(Res1, JsonRequestBehavior.AllowGet);
        }

        private void PopulateDropdowns()
        {
            var items = new List<SelectListItem>
    {
        new SelectListItem { Text = "Aadhar Card", Value = "Aadhar Card" },
        new SelectListItem { Text = "Pan Card", Value = "Pan Card" },
        new SelectListItem { Text = "PassPort", Value = "PassPort" }
    };
            ViewBag.DropdownItems = items;

            List<SelectCountryState> Res = new List<SelectCountryState>();
            DataSet DS = ul.fn_DataSet("SelectCountry");
            var SCS = DS.Tables[0];
            var Res1 = SCS.AsEnumerable().Select(s => new SelectCountryState
            {
                Country_name = s.Field<string>("Country_name")
            }).ToList();
            ViewBag.SelectCountry = new SelectList(Res1, "Country_name", "Country_name");
        }

        //------------------------------------FUNCTIONS--------------------------------------------

        private void SendEmail(string Email)
        {
            Random random = new Random();
            int value = random.Next(100001, 999999);
            TempData["otpGenerated"] = value;

            String from, pass, to, messageBody;

            MailMessage message = new MailMessage();
            to = Email;
            from = "skillupbharat@gmail.com";
            pass = "glticvbacsmmvjkb";
            messageBody = "Your OTP is " + value;
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "OTP Verification";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                SqlParameter[] parameter = new SqlParameter[]
                {
                            new SqlParameter("@OTP",value),
                            new SqlParameter("@Seller_ID", DecodeFromBase64UrlSafe(Request.Cookies[EncodeToBase64UrlSafe("Seller_ID")].Value))

                };
                var isValid = (int)ul.func_ExecuteScalar("InsertOTP", parameter);
                if (isValid > 0)
                {
                    TempData["sucessmessage"] = "Code Send Successfully";
                }

            }
            catch (Exception ex)
            {
                TempData["failmessage"] = ex.Message;
            }

        }
        private int SendEmails(string Email)
        {
            Random random = new Random();
            int value = random.Next(100001, 999999);
            TempData["otp"] = value;

            String from, pass, to, messageBody;

            MailMessage message = new MailMessage();
            to = Email;
            from = "skillupbharat@gmail.com";
            pass = "glticvbacsmmvjkb";
            messageBody = "Your OTP is " + value;
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "OTP Verification";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                SqlParameter[] param = new SqlParameter[]
                        {
                            new SqlParameter("@Email",Email),

                        };
                var isValid = (int)ul.func_ExecuteScalar("checkCustomerEmail", param);
                if (isValid > 0)
                {
                    TempData["sucessmessage"] = "Code Send Successfully";
                    return value;
                }

            }
            catch (Exception ex)
            {
                TempData["failmessage"] = ex.Message;
                return 0;
            }

            return 0;
        }
        private void generateCookie()
        {
            string encodedCookieName = EncodeToBase64UrlSafe("pnmskrbr");
            HttpCookie cookie = Request.Cookies[encodedCookieName];

            if (cookie == null)
            {
                var Res = ul.fn_DataTable("RandomNumber").AsEnumerable().Select(s => new
                {
                    RandomNumber = s.Field<string>("RandomNumber")
                }).FirstOrDefault();

                if (Res != null)
                {
                    cookie = new HttpCookie(encodedCookieName);

                    cookie.Value = EncodeToBase64UrlSafe(Res.RandomNumber);
                    cookie.Expires = DateTime.Now.AddYears(1); 
                    Response.Cookies.Add(cookie); 

                    FormsAuthentication.SetAuthCookie(EncodeToBase64UrlSafe(Res.RandomNumber), true);
                    Session["IsCookieSet"] = true; 
                }
            }
            else
            {
                string randomNumber = cookie.Value;
                FormsAuthentication.SetAuthCookie(randomNumber, true);
            }
        }
        private void CustomerName()
        {
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerID");
            HttpCookie cookie = Request.Cookies[encodedCookieName];
            if (cookie != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);

                SqlParameter[] param = new SqlParameter[]
                {
                new SqlParameter("@CustomerID", decodeCookieName)

                };
                DataSet DSB = ul.fn_DataSet("getCustomerDetail", param);
                var Category1 = DSB.Tables[0];
                var Res2 = Category1.AsEnumerable().Select(s => new CustomerSignup
                {
                    CreatedDate = s.Field<string>("CreatedDate"),
                    FullName = s.Field<string>("FullName"),

                    Email = s.Field<string>("Email"),

                }).FirstOrDefault();
                if (Res2 != null)
                {
                    ViewBag.CustomerName = Res2.FullName;
                }
            }
        }
        private void JoinedDate()
        {
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerID");
            HttpCookie cookie = Request.Cookies[encodedCookieName];
            if (cookie != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);

                SqlParameter[] param = new SqlParameter[]
            {
            new SqlParameter("@CustomerID", decodeCookieName)

            };
                DataSet DSA = ul.fn_DataSet("getCustomerDetail", param);
                var Category = DSA.Tables[0];
                var Res2 = Category.AsEnumerable().Select(s => new CustomerSignup
                {
                    CreatedDate = s.Field<string>("CreatedDate"),
                    FullName = s.Field<string>("FullName"),

                    Email = s.Field<string>("Email"),

                }).FirstOrDefault();

                ViewBag.CreatedDate = Res2.CreatedDate;
            }
        }

        //------------------------------------ENCRYPT & DECRPT CODE---------------------------------
        public static string ComputeSha256Hash(string Password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute hash from the UTF-8 encoded raw data
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));

                // Convert byte array to a string representation (hexadecimal format)
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // "x2" formats byte to hexadecimal string
                }
                return builder.ToString();
            }
        }

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

        //-------------------------------------ACTIONRESULTS-----------------------------------------
        public ActionResult Index()
        {
            generateCookie();
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerTokenID");
            HttpCookie cookie = Request.Cookies[encodedCookieName];
          
            if (cookie != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);
                ViewBag.DecodedTokenID = decodeCookieName;
                CustomerName();
            }
         
        
            return View();
        }

        [Route("Cart")]
        public ActionResult Cart()
        {
            ViewBag.Message = "Your Cart page.";

            return View();
        }

        [Route("Checkout")]
        public ActionResult Checkout()
        {
            ViewBag.Message = "Your Checkout page.";

            return View();
        }

        [Route("CustomerSignup")]
        public ActionResult CustomerSignup()
        {
            string encodedCookie = EncodeToBase64UrlSafe("pnmskrbr");
            HttpCookie cookie = Request.Cookies[encodedCookie];
            if (cookie != null) { 
                   CustomerName();
            }
            //ViewBag.CartTotal = GetCartItemCount();
            //GetCategory();
            return View();
        }

        [Route("CustomerSignup")]
        [HttpPost]
        public ActionResult CustomerSignup(CustomerSignup data)
        {
            //ViewBag.CartTotal = GetCartItemCount();
            //GetCategory();
            string encodedCustomerIDCookie = EncodeToBase64UrlSafe("pnmskrbr");
            HttpCookie cookie = Request.Cookies[encodedCustomerIDCookie];
            string decodedCustomerID = DecodeFromBase64UrlSafe(cookie.Value);

            if (!string.IsNullOrEmpty(data.FullName))
            {
                data.FullName = char.ToUpper(data.FullName[0]) + data.FullName.Substring(1).ToLower();
            }
            string hashedPassword = ComputeSha256Hash(data.Password);

            SqlParameter[] param = new SqlParameter[]
            {
            new SqlParameter("@Email", data.Email),
            new SqlParameter("@Password", hashedPassword),
            new SqlParameter("@FullName", data.FullName),
          //  new SqlParameter("@Role", data.Role),
            new SqlParameter("@RanCookies", decodedCustomerID)

            };

            var dataTable = ul.fn_DataTable("CustomerSignup", param);

            var Res3 = dataTable.AsEnumerable().Select(s => new
            {
                alreadyexist = s.Field<int>("alreadyexist")
            }).FirstOrDefault();

            if (Res3 != null)
            {
                if (Res3.alreadyexist == 2)
                {
                    TempData["fail"] = "Email and Password already exist!";
                }
                else if (Res3.alreadyexist == 1)
                {
                    TempData["fail"] = "Email already exists!";
                }
                else if (Res3.alreadyexist == 3)
                {
                    TempData["fail"] = "Password already exists!";
                }
                else
                {
                    var customerDetails = dataTable.AsEnumerable().Select(s => new
                    {

                        CustomerToken = s.Field<string>("CustomerToken"),
                        FullName = s.Field<string>("FullName"),
                        
                        CustomerID = s.Field<string>("CustomerID")
                    }).FirstOrDefault();

                  
                    if (customerDetails != null)
                    {
                        ViewBag.CustomerName = customerDetails.FullName;

                        string Customer_Name = EncodeToBase64UrlSafe("CustomerName");
                        string Customer_TokenID = EncodeToBase64UrlSafe("CustomerTokenID");
                        string Customer_Role = EncodeToBase64UrlSafe("CustomerRole");
                        string Customer_ID = EncodeToBase64UrlSafe("CustomerID");
                        string Name = EncodeToBase64UrlSafe(customerDetails.FullName);
                        string CustomerIDs = EncodeToBase64UrlSafe(customerDetails.CustomerID);
                        string TokenID = EncodeToBase64UrlSafe(customerDetails.CustomerToken);
                        string Customer = EncodeToBase64UrlSafe("Customer");

                        //HttpCookie cookie = new HttpCookie(encodedName, encodedValue);

                        HttpCookie cookie1 = new HttpCookie(Customer_Name, Name);
                        HttpCookie cookie2 = new HttpCookie(Customer_ID, CustomerIDs);
                        HttpCookie cookie3 = new HttpCookie(Customer_TokenID, TokenID);
                        HttpCookie cookie4 = new HttpCookie(Customer_Role, Customer);

                        cookie1.Expires = DateTime.Now.AddDays(30);
                        cookie2.Expires = DateTime.Now.AddDays(30);
                        cookie3.Expires = DateTime.Now.AddDays(30);
                        cookie4.Expires = DateTime.Now.AddDays(30);

                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                        Response.Cookies.Add(cookie3);
                        Response.Cookies.Add(cookie4);

                        TempData["success"] = "Saved Successfully!";
                        int value = SendEmails(data.Email);
                        if (value > 0)
                        {
                            TempData.Keep("otp");

                            TempData["Sellersuccess"] = "Data Saved Successfully!";
                            return RedirectToAction("CustomerSignupVerifyOTP");
                        }
                        else
                        {
                            TempData["Sellersuccess"] = " Please Enter Valid Email !";
                            return View();
                        }
                    }
                }
            }


            return View();
        }

        [Route("CustomerSignupVerifyOTP")]
        public ActionResult CustomerSignupVerifyOTP()
        {
            
            return View();
        }

        [Route("CustomerSignupVerifyOTP")]
        [HttpPost]

        public ActionResult CustomerSignupVerifyOTP(otp model)
        {

            int generatedOtp = Convert.ToInt32(TempData["otp"]);
            string email = TempData["email"] as string;
            TempData.Keep("email");

            if (ModelState.IsValid)
            {
                    if (model.OTP == generatedOtp)
                    {
                        TempData["otpGenerated"] = null;
                        TempData["otpSucess"] = "Success";
                        TempData["email"] = email;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.otpMess = "Invalid OTP";
                        return View();
                    }
            }
            return View();
        }

        [Route("CustomerLogin")]
        public ActionResult CustomerLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            string encodedCookie = EncodeToBase64UrlSafe("pnmskrbr");
            HttpCookie cookie = Request.Cookies[encodedCookie];
            if (cookie != null)
            {
                CustomerName();
            }

            
            return View();
        }

        [Route("CustomerLogin")]
        [HttpPost]
        public ActionResult CustomerLogin(Sellerlogin model, string returnUrl)  
        {
            string encodedCookie = EncodeToBase64UrlSafe("CustomerID");
            HttpCookie cookie = Request.Cookies[encodedCookie];
        
            if (cookie != null)

            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);
                string hashedPassword = ComputeSha256Hash(model.Password);
                SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@Email", model.Email),
            new SqlParameter("@Password", hashedPassword),
            new SqlParameter("@CustomerCookie", decodeCookieName)
                };

                var dataTable = ul.fn_DataTable("CustomerLogin", param);

                var Res3 = dataTable.AsEnumerable().Select(s => new
                {
                    exist = s.Field<int>("exist")
                }).FirstOrDefault();

                if (Res3 != null && Res3.exist == 1)
                {
                    var customerDetails = dataTable.AsEnumerable().Select(s => new
                    {
                        CustomerToken = s.Field<string>("CustomerToken"),
                        FullName = s.Field<string>("FullName"),
                        CustomerID = s.Field<string>("CustomerID"),
                    }).FirstOrDefault();

                   

                    if (customerDetails != null)
                    {
                        ViewBag.CustomerName = customerDetails.FullName;
                        string Customer_Name = EncodeToBase64UrlSafe("CustomerName");
                        string Customer_TokenID = EncodeToBase64UrlSafe("CustomerTokenID");
                        string Customer_Role = EncodeToBase64UrlSafe("CustomerRole");
                        string Customer_ID = EncodeToBase64UrlSafe("pnmskrbr");
                        string Name = EncodeToBase64UrlSafe(customerDetails.FullName);
                        string CustomerIDs = EncodeToBase64UrlSafe(customerDetails.CustomerID);
                        string TokenID = EncodeToBase64UrlSafe(customerDetails.CustomerToken);
                        string Customer = EncodeToBase64UrlSafe("Customer");

                        //HttpCookie cookie = new HttpCookie(encodedName, encodedValue);

                        HttpCookie cookie1 = new HttpCookie(Customer_Name, Name);
                        HttpCookie cookie2 = new HttpCookie(Customer_ID, CustomerIDs);
                        HttpCookie cookie3 = new HttpCookie(Customer_TokenID, TokenID);
                        HttpCookie cookie4 = new HttpCookie(Customer_Role, Customer);

                        cookie1.Expires = DateTime.Now.AddDays(30);
                        cookie2.Expires = DateTime.Now.AddDays(30);
                        cookie3.Expires = DateTime.Now.AddDays(30);
                        cookie4.Expires = DateTime.Now.AddDays(30);

                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                        Response.Cookies.Add(cookie3);
                        Response.Cookies.Add(cookie4);
                        TempData["Success"] = "Successfully logged in";


                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    TempData["Success"] = "SignUp First!";
                }

              //  ViewBag.CartTotal = LoginGetCartItemCount(ViewBag.newCookie);
                return View();
            }
            else
            {
                return RedirectToAction("CustomerSignup", "Home");
            }
        }


        [HttpPost]
        [Route("CustomerLogout")]
        public ActionResult CustomerLogout()
        {
            FormsAuthentication.SignOut();
            string Customer_Name = EncodeToBase64UrlSafe("CustomerName");
            string Customer_TokenID = EncodeToBase64UrlSafe("CustomerTokenID");
            string Customer_Role = EncodeToBase64UrlSafe("CustomerRole");
            string Customer_ID = EncodeToBase64UrlSafe("pnmskrbr");
            if (Customer_ID != null)
            {
             
                var pnmskrbrCookie = new HttpCookie(Customer_ID)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(pnmskrbrCookie);
            }

            if (Customer_Role != null)
            {
                var customerTokenCookie = new HttpCookie(Customer_Role)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(customerTokenCookie);
            }
            if (Customer_TokenID != null)
            {
                var customerRoleCookie = new HttpCookie(Customer_TokenID)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(customerRoleCookie);
            }
            if (Customer_Name != null)
            {
                var customerNameCookie = new HttpCookie(Customer_Name)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(customerNameCookie);
            }
            Session.Clear();
            Session.Abandon();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

       

        [Route("EditCustomerProfile")]
        [CustomerAuthorization]
        public ActionResult EditCustomerProfile()
        {
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerTokenID");
            HttpCookie cookie = Request.Cookies[encodedCookieName];

            if (cookie != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);
                ViewBag.DecodedTokenID = decodeCookieName;
                CustomerName();
            }
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            //ViewBag.CartTotal = GetCartItemCount();
            //GetCategory();
            if (cookie != null)
            {
                string encodedCookieNames = EncodeToBase64UrlSafe("CustomerID");
                HttpCookie cookies = Request.Cookies[encodedCookieNames];
                string decodeCookieName = DecodeFromBase64UrlSafe(cookies.Value);
                SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@CustomerID", decodeCookieName)

        };
                DataSet DSA = ul.fn_DataSet("getCustomerDetail", param);
                var Category = DSA.Tables[0];
                var Res2 = Category.AsEnumerable().Select(s => new CustomerSignup
                {
                    CreatedDate = s.Field<string>("CreatedDate"),
                    FullName = s.Field<string>("FullName"),
                    Email = s.Field<string>("Email"),
                }).FirstOrDefault();

                if (Res2 != null)
                {
                    ViewBag.EditCustomer = Res2;
                }
                else
                {
                    ViewBag.EditCustomer = new
                    {
                        CreatedDate = "N/A",
                        FullName = string.Empty,
                        Email = string.Empty
                    };
                }
                ViewBag.SuccessMessage = TempData["Verifyemail"];


                return View(Res2);
            }
           

           
             return View();
        }

        [Route("EditCustomerProfile")]
        [HttpPost]
        [CustomerAuthorization]
        public ActionResult EditCustomerProfile(CustomerSignup model)
        {
        //    ViewBag.CartTotal = GetCartItemCount();
        //    GetCategory();

            if (!string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = char.ToUpper(model.FullName[0]) + model.FullName.Substring(1).ToLower();
            }
            string encodedCustomerIDCookie = EncodeToBase64UrlSafe("pnmskrbr");
            HttpCookie cookie = Request.Cookies[encodedCustomerIDCookie];
            string decodedCustomerID = DecodeFromBase64UrlSafe(cookie.Value);
            if (!string.IsNullOrEmpty(model.FullName))
            {
                SqlParameter[] param1 = new SqlParameter[]
                {
            new SqlParameter("@FullName", model.FullName),
            new SqlParameter("@RanCookies", decodedCustomerID)
                };

                var data = ul.func_ExecuteNonQuery("EditCustomerDetailName", param1);
                if (data > 0)
                {
                    return Json(new { success = true, message = "Name updated successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Name to update email." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Name is required!" });
            }
        }


        [Route("SaveAuthorizeEmail")]
        [HttpPost]
        [CustomerAuthorization]
        public JsonResult SaveAuthorizeEmail(string Email)
        {
       
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerTokenID");
            HttpCookie cookieToken = Request.Cookies[encodedCookieName];

            if (cookieToken != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookieToken.Value);
                ViewBag.DecodedTokenID = decodeCookieName;
                CustomerName();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                       
                        string timestamp = DateTime.UtcNow.ToString("O");

                        string confirmUrl = Url.Action("ConfirmEmail", "Home", new { email = Email, time = timestamp }, Request.Url.Scheme);
                        string rejectUrl = Url.Action("RejectEmail", "Home", new { email = Email, time = timestamp }, Request.Url.Scheme);

                        MailMessage message = new MailMessage
                        {
                            From = new MailAddress("skillupbharat@gmail.com"),
                            Subject = "Confirmation Email from Stationary Spot",
                            Body = $@"
                       Please confirm your email by clicking one of the options below:<br/><br/>
                      <a href='{confirmUrl}' style='display: inline-block; padding: 10px 20px; font-size: 16px; color: white; background-color: green; text-decoration: none; border-radius: 5px;'>Yes, It's me</a>
                        &nbsp;&nbsp;&nbsp;
                     <a href='{rejectUrl}' style='display: inline-block; padding: 10px 20px; font-size: 16px; color: white; background-color: red; text-decoration: none; border-radius: 5px;'>No</a>
                      <br/><br/>Valid only for 1 minute",
                            IsBodyHtml = true
                        };
                        message.To.Add(Email);

                        SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                        {
                            EnableSsl = true,
                            Port = 587,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new NetworkCredential("skillupbharat@gmail.com", "glticvbacsmmvjkb")
                        };

                        try
                        {
                            smtp.Send(message);
                            TempData["successmessage"] = "Code Sent Successfully";
                            return Json(new { success = true, message = "Code sent successfully" });
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, message = ex.Message });
                        }
                    }


                    else
                    {
                        return Json(new { success = false, message = "Please enter a valid email address" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpGet]
        public ActionResult ConfirmEmail(string email, string time)
        {
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerTokenID");
            HttpCookie cookieToken = Request.Cookies[encodedCookieName];

            if (cookieToken != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookieToken.Value);
                ViewBag.DecodedTokenID = decodeCookieName;
                CustomerName();
            }

            DateTime emailSentTime;
            if (!DateTime.TryParse(time, null, System.Globalization.DateTimeStyles.RoundtripKind, out emailSentTime))
            {
                return Content("Invalid time format.");
            }

            if ((DateTime.UtcNow - emailSentTime).TotalMinutes > 1)
            {
                return Content("This link has expired. The email was valid for only one minute.");
            }

            HttpCookie cookie = Request.Cookies[EncodeToBase64UrlSafe("CustomerID")];
            if (cookie != null)
            {
                string decodedCustomerID = DecodeFromBase64UrlSafe(cookie.Value);

                SqlParameter[] param1 = new SqlParameter[]
                {
            new SqlParameter("@Email", email),
            new SqlParameter("@RanCookies", decodedCustomerID)
                };

                var data = ul.func_ExecuteNonQuery("EditCustomerDetailEmail", param1);
                if (data == 0)
                {
                    TempData["successMessage"] = "Email not updated";
                }

              
                FormsAuthentication.SetAuthCookie(email, true); 
            }
            else
            {
                TempData["successMessage"] = "Something went wrong!";
            }

            return RedirectToAction("EditCustomerProfile");
        }



        [HttpGet]
        public ActionResult RejectEmail(string email, string time)
        {
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerTokenID");
            HttpCookie cookieToken = Request.Cookies[encodedCookieName];

            if (cookieToken != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookieToken.Value);
                ViewBag.DecodedTokenID = decodeCookieName;
                CustomerName();
            }

            DateTime emailSentTime;
            if (!DateTime.TryParse(time, null, System.Globalization.DateTimeStyles.RoundtripKind, out emailSentTime))
            {
                return Content("Invalid time format.");
            }
            if ((DateTime.UtcNow - emailSentTime).TotalMinutes > 1)
            {
              
                return Content("This link has expired. The email was valid for only one minute.");
            }
            TempData["failureMessage"] = "EmailNotUpdated";
            return RedirectToAction("EditCustomerProfile");
        }




        [Route("CustomerSecurity")]
        [CustomerAuthorization]
        public ActionResult CustomerSecurity()
        {
            JoinedDate();
            string encodedCookieName = EncodeToBase64UrlSafe("CustomerTokenID");
            HttpCookie cookie = Request.Cookies[encodedCookieName];

            if (cookie != null)
            {
                string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);
                ViewBag.DecodedTokenID = decodeCookieName;
                CustomerName();
            }
            ViewBag.CurrentFormStep = "emailFormSection";
            //ViewBag.CartTotal = GetCartItemCount();
            //GetCategory();
            return View();

        }

        [Route("CustomerSecurity")]
        [HttpPost]
        public string CustomerSecurity(string Email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                       
                        string encodedCookieName = EncodeToBase64UrlSafe("CustomerID");
                        HttpCookie cookie = Request.Cookies[encodedCookieName];
                        if (cookie != null)
                        {
                            string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);

                            SqlParameter[] param = new SqlParameter[]
                        {
                               new SqlParameter("@Email", Email),
                               new SqlParameter("@CustomerCookie", decodeCookieName)
                        };

                            var isValid = (int)ul.func_ExecuteScalar("checkEmailSecurity", param);
                            if (isValid > 0)
                            {
                                try
                                {
                                    int code = SendEmails(Email);
                                    if (code != 0)
                                    {
                                        DateTime otpGenerationTime = DateTime.Now;
                                        TempData["otpGenerationTimeS"] = otpGenerationTime;
                                        // TempData.Keep("otpS") ;
                                        TempData["otpS"] = code;
                                        TempData["sucessmessage"] = "CodeSent";
                                        
                                        return "true";
                                      //  return Json(new { success = true, message = "Code sent successfully" });
                                    }
                                    else
                                    {
                                        string msg = "Something went wrong !";
                                        return msg;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ex.Message;
                                }
                            }
                            else
                            {
                                string msg1 = "Email does not exist";
                                return msg1;
                            }
                        }
                        string msg2 = "Email does not exist";
                        return msg2;
                    }
                    else
                    {
                        string msg3 = "Please enter a valid email address";
                        return msg3;
                       
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            string msg4 = "Invalid data";
            return msg4;
        }



        [Route("CustomerPassChange")]
        [HttpPost]
        public ActionResult CustomerPassChange(string pass)
        {

            if (!string.IsNullOrEmpty(pass))
            {
                string hashedPassword = ComputeSha256Hash(pass);
                string encodedCookieName = EncodeToBase64UrlSafe("CustomerID");
                HttpCookie cookie = Request.Cookies[encodedCookieName];
                if (cookie != null)
                {
                    string decodeCookieName = DecodeFromBase64UrlSafe(cookie.Value);

                    SqlParameter[] param = new SqlParameter[]
                {
            new SqlParameter("@pass", hashedPassword),
            new SqlParameter("@CustomerCookie", decodeCookieName)
                };

                    var isValid = (int)ul.func_ExecuteScalar("updateSecurityPass", param);
                    if (isValid > 0)
                    {
                        try
                        {
                            return Json(new { success = true, message = "Your password has been updated!" });
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, message = ex.Message });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Invalid Password" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Cookie is empty !" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Please enter a valid password!" });
            }
        }

        [Route("CustomerSecurityOTP")]
        [HttpPost]
        public string CustomerSecurityOTP(int? otp)
        {
            TempData.Keep("otpS");
            // var otp = model.OTP;
            if (otp != null)
            {
                try
                {
                    int generatedOtp = Convert.ToInt32(TempData["otpS"]);
                    DateTime generationTime = (TempData["otpGenerationTimeS"] as DateTime?) ?? DateTime.MinValue;
                    DateTime currentTime = DateTime.Now;
                    if ((currentTime - generationTime).TotalMinutes <= 1)
                    {
                        if (otp == generatedOtp)
                        {
                            TempData["otpGenerated"] = null;
                            TempData["otpSuccess"] = "Success";
                            return "true";
                        }
                        else
                        {
                            ViewBag.otpMess = "InvalidOTP";
                            return "InvalidOTP";
                        }
                    }
                    else
                    {
                        ViewBag.otpMess = "Expired";
                        return "Expired";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.otpMess = "ex.Message";
                    return ex.Message;
                }
            }
            else
            {
                ViewBag.otpMess = "OTPIsRequired";
                return "OTPIsRequired";
            }

        }


        [HttpGet]
        public ActionResult GenerateCaptcha()
        {
            // Generate a random CAPTCHA code
            string captchaCode = GenerateRandomCode(6);
            TempData["CaptchaCode"] = captchaCode;
            TempData.Keep("CaptchaCode");

            // Convert the CAPTCHA code to an image
            var captchaImage = CreateComplexCaptchaImage(captchaCode);
            return File(captchaImage, "image/png");
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            Random random = new Random();
            char[] code = new char[length];
            for (int i = 0; i < length; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }
            return new string(code);
        }

        private byte[] CreateComplexCaptchaImage(string captchaCode)
        {
            int width = 500; // Increased width for horizontal orientation
            int height = 100; // Height is kept smaller for horizontal layout

            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bitmap))
            using (MemoryStream ms = new MemoryStream())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                // Draw CAPTCHA code with random font, size, and color
                Random rand = new Random();
                var fonts = new[] { "Arial", "Georgia", "Comic Sans MS", "Times New Roman", "Verdana" };
                for (int i = 0; i < captchaCode.Length; i++)
                {
                    using (Font font = new Font(fonts[rand.Next(fonts.Length)], rand.Next(30, 45), FontStyle.Bold))
                    using (Brush brush = new SolidBrush(Color.FromArgb(rand.Next(0, 150), rand.Next(0, 150), rand.Next(0, 150))))
                    {
                        float x = 20 + i * 50 + rand.Next(-10, 10); // Adjust spacing for horizontal layout
                        float y = rand.Next(20, 40);
                        g.DrawString(captchaCode[i].ToString(), font, brush, new PointF(x, y));
                    }
                }

                // Add random lines for added complexity
                for (int i = 0; i < 12; i++) // Increased the number of lines for more complexity
                {
                    int x1 = rand.Next(bitmap.Width);
                    int y1 = rand.Next(bitmap.Height);
                    int x2 = rand.Next(bitmap.Width);
                    int y2 = rand.Next(bitmap.Height);
                    using (Pen pen = new Pen(Color.FromArgb(rand.Next(150, 255), rand.Next(150, 255), rand.Next(150, 255)), 3))
                    {
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

                // Apply distortion effect
                Bitmap distortedBitmap = ApplyDistortion(bitmap);

                // Save the image to memory stream
                distortedBitmap.Save(ms, ImageFormat.Png);

                return ms.ToArray();
            }
        }

        private Bitmap ApplyDistortion(Bitmap bitmap)
        {
            int wave = 10;
            Bitmap distorted = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics g = Graphics.FromImage(distorted))
            {
                g.Clear(Color.White);
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int newX = (int)(x + (wave * Math.Sin(2 * Math.PI * y / 128.0)));
                        int newY = (int)(y + (wave * Math.Cos(2 * Math.PI * x / 128.0)));
                        if (newX < 0 || newX >= bitmap.Width)
                            newX = 0;
                        if (newY < 0 || newY >= bitmap.Height)
                            newY = 0;
                        distorted.SetPixel(x, y, bitmap.GetPixel(newX, newY));
                    }
                }
            }
            return distorted;
        }

      
        [Route("CustomerpassCapcha")]
        [HttpPost]
        public JsonResult CustomerpassCapcha(string Capcha)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(Capcha))
            {
                string storedCaptcha = TempData["CaptchaCode"] as string;

                if (storedCaptcha != null && Capcha == storedCaptcha)
                {
                    result = true;
                }
                else
                {
                    TempData["CaptchaCode"] = null;
                }
            }

            // Return the result as JSON
            return Json(result);
        }





        [Route("SellerProfile")]
        [SellerAuthorization]
        public ActionResult SellerProfile()
        {
            return View();
        }





//      \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Route("BecomeSeller")]
        public ActionResult BecomeSeller()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("PaymentCycle")]
        public ActionResult PaymentCycle()
        {
            ViewBag.Message = "Your Payment Cycle page.";

            return View();
        }
        [Route("SupportTeam")]
        public ActionResult SupportTeam()
        {
            ViewBag.Message = "Your Support Team page.";

            return View();
        }
   

        [Route("SellerSignup")]
        public ActionResult SellerSignup()
        {
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                if (decodedSellerRole == "Sellers")
                {
                    ViewBag.Message = "Your application Add Product page.";

                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);
                        ViewBag.sellerInfo = decodedSellerName;
                    }

                    string encodedSellerIDCookieName = EncodeToBase64UrlSafe("Seller_ID");
                    HttpCookie sellerIDCookie = Request.Cookies[encodedSellerIDCookieName];

                    if (sellerIDCookie != null && !string.IsNullOrEmpty(sellerIDCookie.Value))
                    {
                        string sellerID = sellerIDCookie.Value; // Replace with your logic to get the seller ID
                        string url = $"/AddProduct/{sellerID}";

                        return Redirect(url);
                    }
                    else
                    {
                        PopulateDropdowns();
                        return View();
                    }
                }
            }

            ViewBag.Message = "Sellers ID cookie is missing or empty.";
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [Route("SellerSignup")]
        public ActionResult SellerSignup(SellerSignup model)
        {
            var items = new List<SelectListItem>
        {
            new SelectListItem { Text = "Aadhar Card", Value = "Aadhar Card" },
            new SelectListItem { Text = "Pan Card", Value = "Pan Card" },
            new SelectListItem { Text = "PassPort", Value = "PassPort" }

        };
            ViewBag.DropdownItems = items;

            List<SelectCountryState> Res = new List<SelectCountryState>();

            DataSet DS = ul.fn_DataSet("SelectCountry");
            var SCS = DS.Tables[0];
            var Res1 = SCS.AsEnumerable().Select(s => new SelectCountryState
            {

                Country_name = s.Field<string>("Country_name"),

            }).ToList();
            ViewBag.SelectCountry = new SelectList(Res1, "Country_name", "Country_name");

            if (ModelState.IsValid)
            {
                try
                {

                    string hashedPassword = ComputeSha256Hash(model.Password);
                    List<SellerSignup> Res3 = new List<SellerSignup>();
                    SqlParameter[] param = new SqlParameter[]
                    {
                   new SqlParameter("@Name",model.Name),
                   new SqlParameter("@Email",model.Email),
                   new SqlParameter("@Gender",model.Gender),
                   new SqlParameter("@Password",hashedPassword),
                   new SqlParameter("@MobileNumber",model.MobileNumber),
                   new SqlParameter("@IDProofType",model.IDProofType),
                   new SqlParameter("@IDProof",model.IDProof),
                   new SqlParameter("@SellerAddress",model.SellerAddress),
                   new SqlParameter("@SellerState",model.SellerState),
                   new SqlParameter("@SellerCountry",model.SellerCountry),
                   new SqlParameter("@SellerPincode",model.SellerPincode),
                   new SqlParameter("@BankName",model.BankName),
                   new SqlParameter("@IFSECode",model.IFSECode),
                   new SqlParameter("@AccountNo",model.AccountNo),
                   new SqlParameter("@CompanyName",model.CompanyName),
                   new SqlParameter("@CompanyGST",model.CompanyGST),
                   new SqlParameter("@CompanyAddress",model.CompanyAddress),
                   new SqlParameter("@CompanyEmail",model.CompanyEmail),
                   new SqlParameter("@CompanyPhone",model.CompanyPhone)

                    };
                    var dataTable = ul.fn_DataTable("SellerSignUp", param);

                    var data = dataTable.AsEnumerable().Select(s => new
                    {
                        alreadyexist = s.Field<int>("alreadyexist")
                    }).FirstOrDefault();

                    if (data != null)
                    {
                        if (data.alreadyexist == 2)
                        {
                            TempData["fail"] = "Email and Password already exist!";
                        }
                        else if (data.alreadyexist == 1)
                        {
                            TempData["fail"] = "Email already exists!";
                        }
                        else if (data.alreadyexist == 3)
                        {
                            TempData["fail"] = "Password already exists!";
                        }
                        else
                        {

                         
                            var Result = dataTable.AsEnumerable().Select(s => new
                            {
                                Seller_ID = s.Field<string>("SellerID"),
                                Name = s.Field<string>("Name"),
                                TokenID = s.Field<string>("SellerToken")
                            }).FirstOrDefault();
                            ViewBag.sellerInfo = Result;
                            string Seller_Name = EncodeToBase64UrlSafe("Seller_Name");
                            string Seller_TokenID = EncodeToBase64UrlSafe("Seller_TokenID");
                            string Seller_Role = EncodeToBase64UrlSafe("Seller_Role");
                            string Seller_ID = EncodeToBase64UrlSafe("Seller_ID");
                            string Name = EncodeToBase64UrlSafe(Result.Name);
                            string SellerIDs = EncodeToBase64UrlSafe(Result.Seller_ID);
                            string TokenID = EncodeToBase64UrlSafe(Result.TokenID);
                            string Sellers = EncodeToBase64UrlSafe("Sellers");

                           

                            HttpCookie cookie1 = new HttpCookie(Seller_Name, Name);
                            HttpCookie cookie2 = new HttpCookie(Seller_ID, SellerIDs);
                            HttpCookie cookie3 = new HttpCookie(Seller_TokenID, TokenID);
                            HttpCookie cookie4 = new HttpCookie(Seller_Role, Sellers);

                            cookie1.Expires = DateTime.Now.AddDays(30);
                            cookie2.Expires = DateTime.Now.AddDays(30);
                            cookie3.Expires = DateTime.Now.AddDays(30);
                            cookie4.Expires = DateTime.Now.AddDays(30);

                            Response.Cookies.Add(cookie1);
                            Response.Cookies.Add(cookie2);
                            Response.Cookies.Add(cookie3);
                            Response.Cookies.Add(cookie4);

                            //-----------------------------------SellerEmailVerify----------------------------------
                            SendEmail(model.Email);
                            ModelState.Clear();
                            return Redirect("VerifyOTP");
                        }

                    }
                    }
                catch (Exception ex)
                {

                    TempData["alert"] = "Please Enter all field correctly!";
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                TempData["alert"] = "Please Enter all field correctly!";
            }

            return View();
        }

        [Route("VerifyOTP")]
        public ActionResult VerifyOTP()
        {
            ViewBag.Message = "Your Verify OTP page.";

            return View();
        }

        [HttpPost]
        [Route("VerifyOTP")]
        public ActionResult VerifyOTP(VerifyOTP model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string Sellers = EncodeToBase64UrlSafe("Seller_ID");


                    //HttpCookie cookie = Request.Cookies[encodedName];
                    HttpCookie cookie = Request.Cookies[Sellers];
                    // Decode the cookie value from Base64
                    string decodedValue = DecodeFromBase64UrlSafe(cookie.Value);
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@OTP",model.OTP),
                        new SqlParameter("@SellerID", decodedValue)
                    };
                    var isValid = (int)ul.func_ExecuteScalar("VerifyOTP", param);
                    if (isValid > 0)
                    {
                        ModelState.Clear();
                        string sellerID = cookie.Value;
                        string url = $"/AddProduct/{sellerID}";

                        return Redirect(url);

                    }
                }
                catch (Exception ex)
                {
                    TempData["messages"] = ex.Message;
                    TempData["otpalert"] = "Please Enter Correct OTP";
                    return View();

                }
            }
            return View();
        }

        [Route("Sellerlogin")]
        public ActionResult SellerLogin(string ReturnURL)
        {
            ViewBag.ReturnURL = ReturnURL;
            // Encode the cookie names to Base64 URL-safe
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;
                    }

                    string encodedSellerIDCookieName = EncodeToBase64UrlSafe("Seller_ID");
                    HttpCookie sellerIDCookie = Request.Cookies[encodedSellerIDCookieName];

                    if (sellerIDCookie != null && !string.IsNullOrEmpty(sellerIDCookie.Value))
                    {
                        // Redirect to AddProduct action with Seller_ID
                        //return RedirectToAction(`AddProduct/${Sellers}`, "Home", new { Sellers = sellerIDCookie.Value });
                        string sellerID = sellerIDCookie.Value; // Replace with your logic to get the seller ID
                        string url = $"/AddProduct/{sellerID}";

                        return Redirect(url);
                    }
                    else
                    {
                        // Handle the case where Seller_ID cookie is missing or empty
                        ViewBag.Message = "Sellers ID cookie is missing or empty.";
                        return View();
                    }
                }
            }

            // Set the message for the Sellers Login page if the seller role is not "Sellers" or the cookie is missing
            ViewBag.Message = "Your Sellers Login page.";
            return View();
        }

        [HttpPost]
        [Route("Sellerlogin")]
        public ActionResult Sellerlogin(Sellerlogin model, string ReturnURL)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    string hashedPassword = ComputeSha256Hash(model.Password);
                    List<SellerSignup> Res3 = new List<SellerSignup>();
                    SqlParameter[] param = new SqlParameter[]
                    {

                        new SqlParameter("@Email",model.Email),
                        new SqlParameter("@Password",hashedPassword)
                    };
                    var dataTable = ul.fn_DataTable("SellerLogin", param);

                    var data = dataTable.AsEnumerable().Select(s => new
                    {
                        exist = s.Field<int>("exist")
                    }).FirstOrDefault();

                    if (data != null && data.exist == 1)
                    {
                        var Result = dataTable.AsEnumerable().Select(s => new
                        {
                            Seller_ID = s.IsNull("SellerID") ? null : s.Field<string>("SellerID"),
                            Name = s.IsNull("Name") ? null : s.Field<string>("Name"),
                            TokenID = s.IsNull("SellerToken") ? null : s.Field<string>("SellerToken")
                        }).FirstOrDefault();

                        if (Result.Seller_ID != null && Result.TokenID != null && Result.Name != null)
                        {
                            ViewBag.sellerInfo = Result.Name;

                            string Seller_Name = EncodeToBase64UrlSafe("Seller_Name");
                            string Seller_TokenID = EncodeToBase64UrlSafe("Seller_TokenID");
                            string Seller_Role = EncodeToBase64UrlSafe("Seller_Role");
                            string Seller_ID = EncodeToBase64UrlSafe("Seller_ID");
                            string Name = EncodeToBase64UrlSafe(Result.Name);
                            string SellerIDs = EncodeToBase64UrlSafe(Result.Seller_ID);
                            string TokenID = EncodeToBase64UrlSafe(Result.TokenID);
                            string Sellers = EncodeToBase64UrlSafe("Sellers");

                            //HttpCookie cookie = new HttpCookie(encodedName, encodedValue);

                            HttpCookie cookie1 = new HttpCookie(Seller_Name, Name);
                            HttpCookie cookie2 = new HttpCookie(Seller_ID, SellerIDs);
                            HttpCookie cookie3 = new HttpCookie(Seller_TokenID, TokenID);
                            HttpCookie cookie4 = new HttpCookie(Seller_Role, Sellers);

                            cookie1.Expires = DateTime.Now.AddDays(30);
                            cookie2.Expires = DateTime.Now.AddDays(30);
                            cookie3.Expires = DateTime.Now.AddDays(30);
                            cookie4.Expires = DateTime.Now.AddDays(30);

                            Response.Cookies.Add(cookie1);
                            Response.Cookies.Add(cookie2);
                            Response.Cookies.Add(cookie3);
                            Response.Cookies.Add(cookie4);

                            ModelState.Clear();
                            string Seller = EncodeToBase64UrlSafe("Seller_ID");

                            HttpCookie cookie = Request.Cookies[Seller];

                            if (!string.IsNullOrEmpty(ReturnURL))
                            {
                                return Redirect(ReturnURL);
                            }
                            else
                            {
                                string sellerID = cookie.Value;
                                string url = $"/AddProduct/{sellerID}";

                                return Redirect(url);
                            }
                     
                        }
                        else
                        {
                            TempData["alert"] = "Incorrect Credentials!";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["alert"] = "Incorrect Credentials!";
                        return View();
                    }
                    }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                TempData["alert"] = "Please Enter all field correctly!";
            }

            return View();
        }

    
        [Route("AddProduct/{seller}")] 
        public ActionResult AddProduct(string seller)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            // Encode the cookie names to Base64 URL-safe
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;
                        ViewBag.sellername = seller;
                    }

                    return View();
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("AddProduct/{seller}")]
        public ActionResult AddProduct(AddProduct model)
        {
            string Sellers = EncodeToBase64UrlSafe("Seller_ID");

            HttpCookie cookie = Request.Cookies[Sellers];
            if (ModelState.IsValid)
            {
               // string safeDescription = Sanitizer.GetSafeHtmlFragment(model.Description);

                
                try
                {
                    //string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                    //HttpCookie cookie = Request.Cookies[Sellers];
                    string decodedSellerID = DecodeFromBase64UrlSafe(cookie.Value);

                  //  List<AddProduct> Res = new List<AddProduct>();
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@ProductName",model.ProductName),
                        new SqlParameter("@Description",model.Description),
                        new SqlParameter("@SellerID",decodedSellerID)
                    };
                    DataSet DS = ul.fn_DataSet("AddProduct", param);
                    var Product = DS.Tables[0];
                    var Result = Product.AsEnumerable().Select(s => new
                    {
                        Seller_ID = s.Field<string>("SellerID"),
                        ProductID = s.Field<string>("PID"),
                     
                    }).FirstOrDefault();
                    if (Result.Seller_ID != null && Result.ProductID != null)
                    {
                        ModelState.Clear();
                        TempData["message4"] = "Product Added Successfully";
                        //return RedirectToAction("AddCategory", new RouteValueDictionary(new
                        //{
                        //    Controller = "Home",
                        //    Action = "AddCategory",
                        //    Sellers = Result.Seller_ID,
                        //    Product = Result.ProductID

                        //}));
                        string seller = cookie.Value;
                        string ProductID = EncodeToBase64UrlSafe(Result.ProductID);
                        string URL = $"/AddCategory/{seller}/{ProductID}";

                        return Redirect(URL);

                    }
                    else
                    {  
                        TempData["alert"] = "Please Enter All Fields !";
                    }
                }
                catch (Exception ex)
                {

                    TempData["alert"] = "Please Enter all field correctly!";
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                TempData["alert"] = "Please Enter all field correctly!";
            }

            return View();
        }

     

        [Route("ProductDescription")]
        public ActionResult ProductDescription()
        {
            ViewBag.Message = "Your Product Description page.";

            return View();
        }

  

        [Route("ProductCategory")]
        public ActionResult ProductCategory()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            ViewBag.Message = "Your Product Category page.";
            ViewBag.sellerInfo = Request.Cookies[EncodeToBase64UrlSafe("Seller_Name")].Value;
            return View();
        }
      
        [Route("AddCategory/{seller}/{ProductID}")]
        public ActionResult AddCategory(string seller, string ProductID)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            // Encode the cookie names to Base64 URL-safe
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;
                        ViewBag.sellername = seller;
                    }
                    //ViewBag.sellerInfo = Request.Cookies[EncodeToBase64UrlSafe("Seller_Name")].Value;
                    List<SelectProductCategory> Res = new List<SelectProductCategory>();

                    DataSet DS = ul.fn_DataSet("SelectBussinessCategory");
                    var SBC = DS.Tables[0];
                    var Res1 = SBC.AsEnumerable().Select(s => new SelectProductCategory
                    {

                        BusinessCategory = s.Field<string>("BusinessCategory")

                    }).ToList();
                    ViewBag.SelectBussinessCategory = new SelectList(Res1, "BusinessCategory", "BusinessCategory");
                    return View();
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");
         
        }

        [HttpPost]
        [Route("AddCategory/{seller}/{ProductID}")]
        public ActionResult AddCategory(AddCategory model, string seller, string ProductID)
        {

            string Sellers = EncodeToBase64UrlSafe("Seller_ID");

            ViewBag.sellerInfo = DecodeFromBase64UrlSafe(Request.Cookies[EncodeToBase64UrlSafe("Seller_Name")].Value);
            HttpCookie cookie = Request.Cookies[Sellers];
            if (ModelState.IsValid)
            {
                
                try
                {
                    string decodedSellerID = DecodeFromBase64UrlSafe(cookie.Value);
                    string decodedProductID = DecodeFromBase64UrlSafe(ProductID);
                    List<AddProduct> Res = new List<AddProduct>();
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@BusinessCategory",model.BusinessCategory),
                        new SqlParameter("@SellingCategory",model.SellingCategory),
                        new SqlParameter("@SubCategory",model.SubCategory),
                        new SqlParameter("@ProductID",decodedProductID),
                    };
                    var Result = ul.fn_DataTable("AddCategory", param).AsEnumerable().Select(s => new
                    {
                        CategoryID = s.Field<string>("CID") 
                    }).FirstOrDefault();
                    if (Result.CategoryID != null)
                    {
                        ModelState.Clear();
                        TempData["message4"] = "Product Added Successfully";
                        //return RedirectToAction("AddCategory", new RouteValueDictionary(new
                        //{
                        //    Controller = "Home",
                        //    Action = "AddCategory",
                        //    Sellers = Result.Seller_ID,
                        //    Product = Result.ProductID

                        //}));
                       
                        string categoryID = EncodeToBase64UrlSafe(Result.CategoryID);
                        string URL = $"/AddInventory/{seller}/{ProductID}/{categoryID}";

                        return Redirect(URL);

                    }
                    else
                    {  
                        TempData["alert"] = "Please Enter All Fields !";
                    }
                }
                catch (Exception ex)
                {

                    TempData["alert"] = "Please Enter all field correctly!";
                    throw new Exception(ex.Message);
                    
                }
            }
            else
            {
                TempData["alert"] = "Please Enter all field correctly!";
            }
            return View();

        }

        [Route("AddInventory/{seller}/{ProductID}/{categoryID}")]
        public ActionResult AddInventory(string seller, string ProductID, string categoryID)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            // Encode the cookie names to Base64 URL-safe
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;
                        ViewBag.sellername = seller;
                    }
                    //ViewBag.sellerInfo = Request.Cookies[EncodeToBase64UrlSafe("Seller_Name")].Value;
                    ViewBag.Message = "Your Add Inventory page.";
                    //ViewBag.sellerInfo = Request.Cookies[EncodeToBase64UrlSafe("Seller_Name")].Value;
                    var items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Kilogram (kg)", Value = "Kilogram (kg)" },
                        new SelectListItem { Text = "Dozen", Value = "Dozen" },
                        new SelectListItem { Text = "Piece (pc)", Value = "Piece (pc)" },
                        new SelectListItem { Text = "Litre (L)", Value = "Litre (L)" },
                        new SelectListItem { Text = "Meter (m)", Value = "Meter (m)" },
                        new SelectListItem { Text = "Box", Value = "Box" },
                        new SelectListItem { Text = "Pack", Value = "Pack" },
                        new SelectListItem { Text = "Case", Value = "Case" },
                        new SelectListItem { Text = "Set", Value = "Set" }

                    };
                    ViewBag.DropdownItems = items;
                    return View();
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");

        }

        [HttpPost]
        [Route("AddInventory/{seller}/{ProductID}/{categoryID}")]
        public ActionResult AddInventory(AddInventory model, string seller, string ProductID, string categoryID)
        {
            ViewBag.Message = "Your Add Inventory page.";
            ViewBag.sellerInfo = DecodeFromBase64UrlSafe(Request.Cookies[EncodeToBase64UrlSafe("Seller_Name")].Value);
            var items = new List<SelectListItem>
        {
            new SelectListItem { Text = "Kilogram (kg)", Value = "Kilogram (kg)" },
            new SelectListItem { Text = "Dozen", Value = "Dozen" },
            new SelectListItem { Text = "Piece (pc)", Value = "Piece (pc)" },
            new SelectListItem { Text = "Litre (L)", Value = "Litre (L)" },
            new SelectListItem { Text = "Meter (m)", Value = "Meter (m)" },
            new SelectListItem { Text = "Box", Value = "Box" },
            new SelectListItem { Text = "Pack", Value = "Pack" },
            new SelectListItem { Text = "Case", Value = "Case" },
            new SelectListItem { Text = "Set", Value = "Set" }

        };
            ViewBag.DropdownItems = items;
            string Sellers = EncodeToBase64UrlSafe("Seller_ID");

            HttpCookie cookie = Request.Cookies[Sellers];
            if (ModelState.IsValid)
            {

                try
                {
                    string decodedSellerID = DecodeFromBase64UrlSafe(cookie.Value);
                    string decodedProductID = DecodeFromBase64UrlSafe(ProductID);
                    string decodeCategoryID = DecodeFromBase64UrlSafe(categoryID);
                    List<AddProduct> Res = new List<AddProduct>();
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@ModelName",model.ModelName),
                        new SqlParameter("@Quantity",model.Quantity),
                        new SqlParameter("@Dimension",model.Dimension),
                        new SqlParameter("@StockUnit",model.StockUnit),
                        new SqlParameter("@LiquidContent",model.LiquidContent),
                        new SqlParameter("@AssemblyRequired",model.AssemblyRequired),
                        new SqlParameter("@ManufacturedMaterial",model.ManufacturedMaterial),
                        new SqlParameter("@NumberofSetPerItem",model.NumberofSetPerItem),
                        new SqlParameter("@ProductID",decodedProductID),
                        new SqlParameter("@CategoryID",decodeCategoryID)
                    };
                    var Result = ul.fn_DataTable("AddInventory", param).AsEnumerable().Select(s => new
                    {
                        InventoryID = s.Field<string>("PIID")
                    }).FirstOrDefault();
                    if (Result.InventoryID != null)
                    {
                        ModelState.Clear();
                        TempData["message4"] = "Product Added Successfully";
                        //return RedirectToAction("AddCategory", new RouteValueDictionary(new
                        //{
                        //    Controller = "Home",
                        //    Action = "AddCategory",
                        //    Sellers = Result.Seller_ID,
                        //    Product = Result.ProductID

                        //}));

                        string InventoryID = EncodeToBase64UrlSafe(Result.InventoryID);
                        string URL = $"/ProductInfo/{seller}/{ProductID}/{categoryID}/{InventoryID}";

                        return Redirect(URL);

                    }
                    else
                    {
                        TempData["alert"] = "Please Enter All Fields !";
                    }
                }
                catch (Exception ex)
                {

                    TempData["alert"] = "Please Enter all field correctly!";
                    throw new Exception(ex.Message);

                }
            }
            else
            {
                TempData["alert"] = "Please Enter all field correctly!";
            }
            return View();
        }

        [Route("ProductInfo/{seller}/{ProductID}/{categoryID}/{InventoryID}")]
        public ActionResult ProductInfo(string seller, string ProductID, string categoryID, string InventoryID)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            // Encode the cookie names to Base64 URL-safe
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;
                        ViewBag.sellername = seller;
                    }
                    ViewBag.Message = "Your ProductInfo page.";
                    ViewBag.Value1 = DecodeFromBase64UrlSafe(InventoryID);
                    ViewBag.Value2 = DecodeFromBase64UrlSafe(ProductID);
                    ViewBag.Value3 = InventoryID;
                    ViewBag.Value4 = ProductID;
                    
                    List<AddSubInventory> Res = new List<AddSubInventory>();
                    SqlParameter[] param = new SqlParameter[]
                     {
                   new SqlParameter("@InventoryID",ViewBag.Value1),
                   new SqlParameter("@ProductID", ViewBag.Value2)
                     };
                    DataSet DS = ul.fn_DataSet("SubInventoryInfo", param);
                    var Product = DS.Tables[0];
                    var Res1 = Product.AsEnumerable().Select(s => new AddSubInventory
                    {
                        size = s.Field<string>("size"),
                        color = s.Field<string>("color"),
                        capacity = s.Field<string>("capacity"),
                        piece = s.Field<int>("piece"),
                        price = s.Field<decimal>("price"),
                        PSIID = s.Field<string>("PSIID")
                    }).ToList();
                    ViewBag.SubInventory = Res1;
                    return View();
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");

            
        }

        [Route("ProductImage/{ProductID}/{InventoryID}")]
        public ActionResult ProductImage(string ProductID, string InventoryID)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            // Encode the cookie names to Base64 URL-safe
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        SqlParameter[] paramater = new SqlParameter[]
                     {
                            new SqlParameter("@PIID",DecodeFromBase64UrlSafe(InventoryID)),
                            new SqlParameter("@PID",DecodeFromBase64UrlSafe(ProductID))
                     };
                        ViewBag.sellerInfo = decodedSellerName;
                        DataSet DSa = ul.fn_DataSet("SelectImage",paramater);
                        var SC = DSa.Tables[0];
                        var Res2 = SC.AsEnumerable().Select(s => new AddProductImage
                        {
                            ID = s.Field<int>("ID"),
                            Image = s.Field<string>("PImage"),
                            Color = s.Field<string>("Color")
                            
                         
                        }).ToList();
                        ViewBag.SelectImage = Res2;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];
                       
                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;
                        SqlParameter[] param = new SqlParameter[]
                        {
                            new SqlParameter("@PIID",DecodeFromBase64UrlSafe(InventoryID))
                        };
                        DataSet DS = ul.fn_DataSet("SelectColor", param);
                        var SCS = DS.Tables[0];
                        var Res1 = SCS.AsEnumerable().Select(s => new
                        {
                            Color = s.Field<string>("color")
                        }).ToList();
                        ViewBag.SelectColor = new SelectList(Res1, "color", "color");
                        // Return the view if everything is correct
                        return View();
                    }
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return RedirectToAction("Sellerlogin");
        }

        [HttpPost]
        [Route("ProductImage/{ProductID}/{InventoryID}")]
        public ActionResult ProductImage(AddProductImage model, string ProductID, string InventoryID)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                    HttpCookie cookie = Request.Cookies[Sellers];
                    string decodedSellerID = DecodeFromBase64UrlSafe(cookie.Value);
                    string decodedProductID = DecodeFromBase64UrlSafe(ProductID);
                    string decodedProductIID = DecodeFromBase64UrlSafe(InventoryID);
                    
                    string path = Server.MapPath("~/ProductImages");
                    string fileName = Path.GetFileName(model.File.FileName);
                    string fullPath = Path.Combine(path, fileName);
                    model.File.SaveAs(fullPath);
                    model.ImagePath = "/ProductImages/" + fileName;
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@ProductID",decodedProductID),
                        new SqlParameter("@ProductImage",model.ImagePath),
                        new SqlParameter("@color",model.Color),
                        new SqlParameter("@PIID",decodedProductIID)
                    };
                    var isValid = (int)ul.func_ExecuteScalar("AddProductImage", param);
                    if (isValid > 0)
                    {
                        ModelState.Clear();
                        TempData["message4"] = "Product Added Successfully";

                       string URL = $"/ProductImage/{ProductID}/{InventoryID}";

                        return Redirect(URL);
                        //string URL = $"/AddInventory/{ProductID}/{InventoryID}";

                        //return Redirect(URL);

                    }
                    else
                    {
                        TempData["alert"] = "Please Enter All Fields !";
                    }
                }
                catch (Exception ex)
                {

                    TempData["alert"] = "Please Enter all field correctly!";
                    throw new Exception(ex.Message);
                    
                }
            }
            else
            {
                TempData["alert"] = "Please Enter all field correctly!";
            }
            string URLs = $"/ProductImage/{ProductID}/{InventoryID}";

            return Redirect(URLs);

        }

        [Route("Order/{seller}")]
        public ActionResult Order(string seller)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];

                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;
                    }

                    return View();
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");
        }

        [Route("ProductInventory/{seller}")]
        public ActionResult ProductInventory(string seller)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];

                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;
                        SqlParameter[] param = new SqlParameter[]
                        {
                            new SqlParameter("@Seller",DecodeFromBase64UrlSafe(seller))
                        };
                        DataSet DS = ul.fn_DataSet("Inventory", param);
                        var SCS = DS.Tables[0];
                        var Res1 = SCS.AsEnumerable().Select(s => new ProductInventory
                        {
                            ModelName = s.Field<string>("ModelName"),
                            PName = s.Field<string>("PName"),
                            Quantity = s.Field<int>("Quantity"),
                            Dimension = s.Field<string>("Dimension"),
                            SubCategory = s.Field<string>("SubCategory"),
                            PID = s.Field<string>("PID"),
                            PIID = s.Field<string>("PIID"),
                            CreatedDate = s.Field<string>("CreatedDate")
                        }).ToList();
                        ViewBag.Inventory = Res1;

                        return View();
                    }
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");
        }

        [Route("ProductDocuments/{seller}")]
        public ActionResult ProductDocuments(string seller)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];

                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;

                        SqlParameter[] param = new SqlParameter[]
                        {
                            new SqlParameter("@Seller",DecodeFromBase64UrlSafe(seller))
                        };
                        DataSet DS = ul.fn_DataSet("SellerDocument", param);
                        var SCS = DS.Tables[0];
                        var Res1 = SCS.AsEnumerable().Select(s => new ProductDocument
                        {
                            GSTNumber = s.Field<string>("GSTNumber"),
                            BName = s.Field<string>("BName"),
                            Image = s.Field<string>("BImage"),
                            TotalQuantity = s.Field<int>("TotalQuantity"),
                            TotalPrice = s.Field<decimal>("TotalPrice"),
                            ModeOfPayment = s.Field<string>("ModeOfPayment"),
                            CreatedDate = s.Field<string>("CreatedDate")
                        }).ToList();
                        ViewBag.Document = Res1;

                        return View();
                    }

                }
            }

            // Redirect to Seller login page if the seller role is not "Seller" or if the cookie is not present
            return View("Sellerlogin");
        }

        [Route("PaymentDetails/{seller}")]
        public ActionResult PaymentDetails(string seller)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];

                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;

                        return View();
                    }

                   
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");
        }

        [Route("AddSellerDocument/{seller}")]
        public ActionResult AddSellerDocument(string seller)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];

                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;

                        var items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Online", Value = "Online" },
                        new SelectListItem { Text = "Cash", Value = "Cash" },
                        new SelectListItem { Text = "cheques", Value = "cheques" }

                    };
                        ViewBag.DropdownItems = items;
                        return View();
                    }


                }
            }

            // Redirect to Seller login page if the seller role is not "Seller" or if the cookie is not present
            return View("Sellerlogin");
        }

        [HttpPost]
        [Route("AddSellerDocument/{seller}")]
        public ActionResult AddSellerDocument(ProductDocument model, string seller)
        {
            var items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Online", Value = "Online" },
                        new SelectListItem { Text = "Cash", Value = "Cash" },
                        new SelectListItem { Text = "cheques", Value = "cheques" }

                    };
            ViewBag.DropdownItems = items;
            if (ModelState.IsValid)
            {

                try
                {
                    string Sellers = EncodeToBase64UrlSafe("SellerID");

                    HttpCookie cookie = Request.Cookies[Sellers];
                    string decodedSellerID = DecodeFromBase64UrlSafe(cookie.Value);


                    string path = Server.MapPath("~/ProductImages");
                    string fileName = Path.GetFileName(model.File.FileName);
                    string fullPath = Path.Combine(path, fileName);
                    model.File.SaveAs(fullPath);
                    model.Image = "/ProductImages/" + fileName;
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@Image",model.Image),
                        new SqlParameter("@Seller",DecodeFromBase64UrlSafe(seller)),
                        new SqlParameter("@TotalQuantity",model.TotalQuantity),
                        new SqlParameter("@BName",model.BName),
                        new SqlParameter("@GSTNumber",model.GSTNumber),
                        new SqlParameter("@TotalPrice",model.TotalPrice),
                        new SqlParameter("@ModeOfPayment",model.ModeOfPayment)

                    };
                    var isValid = (int)ul.func_ExecuteScalar("InsertSellerDocument", param);
                    if (isValid > 0)
                    {
                        ModelState.Clear();
                        TempData["message1"] = "Document Added Successfully";
                        TempData["icon1"] = "Success";

                        string URL = $"/ProductDocuments/{seller}";

                        return Redirect(URL);


                    }
                    else
                    {
                        TempData["message1"] = "Something Went Wrong !";
                        TempData["icon1"] = "alert !";
                    }
                }
                catch (Exception ex)
                {

                    TempData["alert"] = "Please Enter all field correctly!";
                    throw new Exception(ex.Message);

                }
            }
            else
            {
                TempData["alert"] = "Please Enter all field correctly!";
            }
            //string URLs = $"/ProductDocuments/{seller}";

            //return Redirect(URLs);
            return View();
        }

        [Route("ProductView/{PID}")]
        public ActionResult ProductView(string PID)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];

                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;
                       // SqlParameter[] param = new SqlParameter[]
                       //{
                       //     new SqlParameter("@PID",DecodeFromBase64UrlSafe(PID))
                       //};
                       // DataSet DS = ul.fn_DataSet("ECommerceInventory", param);
                       // var SCS = DS.Tables[0];
                       // var Res1 = SCS.AsEnumerable().Select(s => new ProductInventory
                       // {
                       //     ModelName = s.Field<string>("ModelName"),
                       //     PName = s.Field<string>("PName"),
                       //     Quantity = s.Field<int>("Quantity"),
                       //     Dimension = s.Field<string>("Dimension"),
                       //     SubCategory = s.Field<string>("SubCategory"),
                       //     PID = s.Field<string>("PID"),
                       //     CreatedDate = s.Field<string>("CreatedDate")
                       // }).ToList();
                       // ViewBag.ProductView = Res1;

                          return View();
                    }


                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");
        }

        [Route("Offer/{seller}/{pid}/{piid}")]
        public ActionResult Offer(string seller, string pid, string piid)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            string encodedSellerRoleCookieName = EncodeToBase64UrlSafe("Seller_Role");
            string encodedSellerNameCookieName = EncodeToBase64UrlSafe("Seller_Name");

            // Retrieve the cookies
            HttpCookie sellerRoleCookie = Request.Cookies[encodedSellerRoleCookieName];
            HttpCookie sellerNameCookie = Request.Cookies[encodedSellerNameCookieName];

            // Check if the seller role cookie is present and has a value
            if (sellerRoleCookie != null && !string.IsNullOrEmpty(sellerRoleCookie.Value))
            {
                // Decode the seller role cookie value
                string decodedSellerRole = DecodeFromBase64UrlSafe(sellerRoleCookie.Value);

                // Check if the decoded seller role is "Sellers"
                if (decodedSellerRole == "Sellers")
                {
                    // Set the message for the Add Product page
                    ViewBag.Message = "Your application Add Product page.";

                    // Check if the seller name cookie is present and has a value
                    if (sellerNameCookie != null && !string.IsNullOrEmpty(sellerNameCookie.Value))
                    {
                        // Decode the seller name cookie value and remove spaces
                        string decodedSellerName = DecodeFromBase64UrlSafe(sellerNameCookie.Value).Replace(" ", string.Empty);

                        // Set the seller info in ViewBag
                        ViewBag.sellerInfo = decodedSellerName;

                        string Sellers = EncodeToBase64UrlSafe("Seller_ID");

                        HttpCookie cookie = Request.Cookies[Sellers];

                        ViewBag.sellerID = cookie.Value;
                        ViewBag.sellername = cookie.Value;
                        List<AddSubInventory> Res = new List<AddSubInventory>();
                        SqlParameter[] param = new SqlParameter[]
                         {
                
                   new SqlParameter("@ProductID", pid),
                   new SqlParameter("@InventoryID", piid)
                         };
                        DataSet DS = ul.fn_DataSet("SubInventoryInfooofer", param);
                        var Product = DS.Tables[0];
                        var Res1 = Product.AsEnumerable().Select(s => new AddSubInventory
                        {
                            size = s.Field<string>("size"),
                            color = s.Field<string>("color"),
                            capacity = s.Field<string>("capacity"),
                            piece = s.Field<int>("piece"),
                            price = s.Field<decimal>("price"),
                            PSIID = s.Field<string>("PSIID")
                        }).ToList();
                        ViewBag.SubInventory = Res1;
                        return View();
                    }
                }
            }

            // Redirect to Sellers login page if the seller role is not "Sellers" or if the cookie is not present
            return View("Sellerlogin");
        }

        [Route("SignOut")]
        public ActionResult SignOut()
        {
            // Sign out using Forms Authentication
            FormsAuthentication.SignOut();

            // List of cookie names to be removed
            var cookieNames = new List<string> { "Seller_ID", "Seller_TokenID", "Seller_Role", "Seller_Name" };

            // Expire each cookie in the list and set its value to null
            foreach (var cookieName in cookieNames)
            {
                // Encode the cookie name to Base64 safely
                string encodedCookieName = EncodeToBase64UrlSafe(cookieName);

                // Check if the cookie exists in the request
                if (Request.Cookies[encodedCookieName] != null)
                {
                    // Create a new cookie with its value set to null and an expiration date in the past
                    var expiredCookie = new HttpCookie(encodedCookieName)
                    {
                        Value = null, // Set cookie value to null
                        Expires = DateTime.Now.AddDays(-1) // Set cookie to expire in the past
                    };

                    // Add the expired cookie to the response to remove it from the browser
                    Response.Cookies.Add(expiredCookie);
                }
            }

            // Clear session data and abandon the session
            Session.Clear();
            Session.Abandon();

            // Redirect to SellerLogin page after signing out
            return RedirectToAction("SellerLogin");
        }
    }
}