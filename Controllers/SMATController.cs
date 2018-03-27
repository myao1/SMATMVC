using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using SMATMVC.Models;

namespace SMATMVC.Controllers{
    public class SMATController : Controller{

        public IActionResult Index(string name, int ID = 0){
            return View();
            //return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }

        public IActionResult Welcome(string name, int numTimes = 1){
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}