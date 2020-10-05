using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RandomNumbers.Controllers     //be sure to use your own project's namespace!
{
    public class HomeController : Controller   //remember inheritance??
    {
        private string generate()
        {
            const string valid = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var builder = new StringBuilder();
            Random rnd = new Random();
            for( var i = 0; i < 14; i++)
            {
                builder.Append(valid[rnd.Next(valid.Length)]);
            }
            return builder.ToString();
        }

        //for each route this controller is to handle:
        [HttpGet("")]       //type of request 
        public IActionResult Index()
        {
            Int32? SessionCount = HttpContext.Session.GetInt32("Counter");
            if (!SessionCount.HasValue)
            {
                HttpContext.Session.SetInt32("Counter" , 1);
                HttpContext.Session.SetString("Password", generate());
            }
            ViewBag.Counter  = HttpContext.Session.GetInt32("Counter");
            ViewBag.Passcode = HttpContext.Session.GetString("Password");

            return View("Index");
        }
        [HttpPost("")]
        public IActionResult Create()
        {
            Int32? SessionCount = HttpContext.Session.GetInt32("Counter");

            HttpContext.Session.SetInt32("Counter" , SessionCount.Value+1);
            HttpContext.Session.SetString("Password", generate());           
            ViewBag.Counter  = HttpContext.Session.GetInt32("Counter");
            ViewBag.Passcode = HttpContext.Session.GetString("Password");
            return View("Index");
        }
    }
}
