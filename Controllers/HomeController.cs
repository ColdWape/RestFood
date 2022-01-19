using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestFood.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseContext _dataBaseContext;

        public HomeController(ILogger<HomeController> logger, DataBaseContext dataBaseContext)
        {
            _logger = logger;
            _dataBaseContext = dataBaseContext;
        }

        public IActionResult Index()
        {
            ViewBag.Dishes = _dataBaseContext.Dishes;

            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
