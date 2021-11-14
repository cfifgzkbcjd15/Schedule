using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Schedule.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FizmatCHGPU.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;

        public HomeController(
           ApplicationContext _db,
           ILogger<HomeController> logger
           )

        {
            db = _db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
            return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult ImportRaspisanie()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult DoImportRaspisanie(IFormFile uploadedFile)
        //{
        //    if (uploadedFile != null)
        //    {
        //        using (var stream = uploadedFile.OpenReadStream())
        //        {
        //            _grabber.ImportRaspisanie(stream);
        //        }
        //    }

        //    return Content("Расписание Загружено");
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
