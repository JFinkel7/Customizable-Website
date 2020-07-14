using MainActivity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MainActivity.Services {
    public class UserAuthentication {
        private List<Claim> claims;
        private ClaimsIdentity claimsIdentity;
        private AuthenticationProperties authProperties;
        private ClaimsPrincipal claimsPrincipal;



        /*** Creating A [User Claim] ***/
        public void CreateClaim(Administrator admin) {
            // Setting Up Claims
            this.claims = new List<Claim> {
                         new Claim(ClaimTypes.Name, admin.Name),
                         new Claim(ClaimTypes.Email, admin.Email),
                         new Claim(ClaimTypes.Role, admin.Role),
                         // With Optional Values
                         
                    };
        }


        // *** Setting A Claim Identity ***/
        public void CreateIdentity() {
            // Setting Up Claims Identity
            this.claimsIdentity =
                 new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }




        public AuthenticationProperties AddAuthProperties() {
            // Create Authentication Property
            this.authProperties = new AuthenticationProperties {
                ExpiresUtc = new DateTimeOffset().AddMinutes(1),

            };
            return (authProperties);
        }


        // Creating A Claims Principle 
        public ClaimsPrincipal GetClaimPrinciple() {
            this.claimsPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });
            return (claimsPrincipal);
        }


    }// CLASS ENDS 
}
