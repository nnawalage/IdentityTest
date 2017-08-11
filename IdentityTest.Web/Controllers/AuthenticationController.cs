﻿using IdentityTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityTest.Web.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Auth
        
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LogInModel() { ReturnUrl=returnUrl};
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.Email == "admin@admin.com" && model.Password == "password")
            {
                var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, "Ben"),
                new Claim(ClaimTypes.Email, "a@b.com"),
                new Claim(ClaimTypes.Country, "England")
            },
                    "AppCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }


        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("AppCookie");
            return RedirectToAction("index", "home");
        }



        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }
    }
}