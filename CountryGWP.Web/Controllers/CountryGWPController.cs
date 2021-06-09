using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CountryGWP.Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace CountryGWP.Web.Controllers
{
    public class CountryGWPController : Controller
    {
        private readonly ILogger<CountryGWPController> _logger;
        private IWebHostEnvironment _hostingEnvironment;

        public CountryGWPController(ILogger<CountryGWPController> logger,  IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            dynamic result = JsonConvert.DeserializeObject("{ 'Name': 'Jon Smith'}");
            return Json(result);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
