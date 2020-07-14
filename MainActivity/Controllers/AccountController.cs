using Microsoft.AspNetCore.Mvc;
using MainActivity.Models;
using MainActivity.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;

namespace MainActivity.Controllers {
    public class AccountController : Controller {
        // Class Fields
        private readonly ICompany<Administrator> companyAdmin;
        private bool IsAdministratorPasswordValid;
        private bool IsAdministratorEmailValid;
        public const string COOKIE_KEY = "Client";

        // Constructor
        public AccountController(ICompany<Administrator> companyAdmin) {
            this.companyAdmin = companyAdmin;
        }




        // <=================== LOGIN ===================>

        [HttpGet]
        public IActionResult Login() {
            return View();

        }

        // Signs-In The User
        [HttpPost]
        public async Task<IActionResult> Login(Company login) {
            if (ModelState.IsValid) {
                // Gets A List Of Registered Adminstrators Within The Database
                IEnumerable<Administrator> allRegisteredAdministrators = companyAdmin.GetAllReferences;
                // Loop Through All Registered Administrators 
                foreach (var registeredAdministrators in allRegisteredAdministrators) {

                    // Check To See If Login Credentials Are Verified
                    IsAdministratorPasswordValid = registeredAdministrators.Password.Equals(login.Administrator.Password);
                    IsAdministratorEmailValid = registeredAdministrators.Email.Equals(login.Administrator.Email);

                    // Checks To See If [INPUT] Password & Email Is Verified 
                    if (IsAdministratorPasswordValid && IsAdministratorEmailValid) {
                        // Adding A Cookie Authentication For Our Logged In Admin
                        UserAuthentication companyAuthentication = new UserAuthentication();
                        companyAuthentication.CreateClaim(registeredAdministrators);
                        companyAuthentication.CreateIdentity();


                        // Creating A Cookie Options
                        CookieOptions cookieOptions = new CookieOptions {
                            // Set Expire Time 
                            Expires = DateTime.Now.AddMinutes(5),
                            IsEssential = true,

                        };


                        // Convert The Object Data Into A [String]
                        string admin = JsonConvert.SerializeObject(registeredAdministrators);

                        // Append The Serialized Data Into The Cookie 
                        HttpContext.Response.Cookies.Append(COOKIE_KEY, admin, cookieOptions);


                        // Adds Sign In Async Cookie Authentication
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                companyAuthentication.GetClaimPrinciple());

                        // Redirects To The Dashboard 
                        return RedirectToAction("Dashboard", "Portal");
                    }
                }
                return View();
            }
            return View();
        }





        // <=================== Register ===================>
        [HttpGet]
        public IActionResult Register() {
            ViewData["Result"] = "GET REQUESTED";
            return View();

        }

        // Register The User To The SQLite Database
        [HttpPost]
        public IActionResult Register(Company model) {
            if (ModelState.IsValid) {
                // Register The Administrator To The Database
                this.companyAdmin.Add(model.Administrator);
                // Redirects Back Home 
                return (RedirectToAction("Index", "Home"));
            }

            return View();
        }


        // Sign-Out The User
        public async Task<IActionResult> Logout() {
            // Sign Out The Current User 
            await HttpContext.SignOutAsync();
            return RedirectToActionPermanent("Index", "Home");
        }



    }// Class Ends
}
