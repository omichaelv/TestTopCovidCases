using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TestTopCovidCases.Models;

namespace TestTopCovidCases.Controllers
{
    public class BindingDropDownController : Controller
    {
        // GET: BindingDropDown
        string Baseurl = "https://covid-19-statistics.p.rapidapi.com/";
        public async System.Threading.Tasks.Task<ActionResult> BindingDropDownAsync()
        {
            
                regionslists RegionsInfo = new regionslists();
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(Baseurl + "regions"),
                    Headers =
                {
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                    { "x-rapidapi-key", "c0aaafc395msh4d87d9a02b6e4bdp18a932jsn6866402cd398" },
                },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var RegionResponse = await response.Content.ReadAsStringAsync();
                    RegionsInfo = JsonConvert.DeserializeObject<regionslists>(RegionResponse);
                }
                return View(RegionsInfo.data);


            //regionslists RegionsInfo = new regionslists();
            //var client = new HttpClient();
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri(Baseurl + "regions"),
            //    Headers =
            //    {
            //        { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
            //        { "x-rapidapi-key", "c0aaafc395msh4d87d9a02b6e4bdp18a932jsn6866402cd398" },
            //    },
            //};
            //using (var response = await client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();
            //    var RegionResponse = await response.Content.ReadAsStringAsync();
            //    RegionsInfo = JsonConvert.DeserializeObject<regionslists>(RegionResponse);
            //}
            //return View(RegionsInfo.regions);

        }
    }
}