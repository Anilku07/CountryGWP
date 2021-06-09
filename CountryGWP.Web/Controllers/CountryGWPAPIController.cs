using CountryGWP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryGWP.Web.Controllers
{
    [ApiController]
    public class CountryGWPAPIController : ControllerBase
    {
        private ICountryGWPService _countryGWPService;
        public CountryGWPAPIController(ICountryGWPService countryGWPService)
        {
            _countryGWPService = countryGWPService;
        }

        [Route("server/api/gwp/avg")]
        [HttpPost]
        public async Task<IActionResult> GetAverageGWP(string country, string lineOfBusiness)
        {
            string[] collection = lineOfBusiness.Split(',');
            string js = "{";
            for (int i = 0; i < collection.Length; i++)
            {
                decimal? avg = _countryGWPService.GetAverageGWP(country, collection[i]);
                js += "'" + collection[i] + "' : " + (avg.HasValue?avg:"null");
                if (i < collection.Length - 1)
                {
                    js += " ,";
                }
            }
            js += " }";
            var result = JsonConvert.DeserializeObject<ExpandoObject>(js) as IDictionary<string, object>;

            return Ok(result);
            //return Ok(result);
            //return View();
        }
    }
}
