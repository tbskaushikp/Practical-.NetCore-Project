﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserRegistration : Controller
    {
        private LoginDbContext db = new LoginDbContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Registration model, string btnClick)
        {
            try
            {
                if (ModelState.IsValid && btnClick == "Submit")
                {
                    db.registrations.Add(model);
                    db.SaveChanges();
                    ViewBag.message = "The user " + model.FirstName + " is saved successfully";
                }
                else if (btnClick == "Reset")
                {
                    ModelState.Clear();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator");
            }
            return View(model);
        }
    }
}
