using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestTopCovidCases.Models;
using TestTopCovidCases.ViewModels;

namespace TestTopCovidCases.Controllers
{
    public class HomeController : Controller
    {

        //WEB API
        string Baseurl = "https://covid-19-statistics.p.rapidapi.com/";

        public async Task<ActionResult> Index()
        {
            CovidCasesViewModel mymodel = new CovidCasesViewModel();
            mymodel.regionslists = await DropdownbindingListAsync();
            mymodel.reports = await GetReports();
            mymodel.columnname = "REGION";
            return View(mymodel);

        }
        [HttpPost]
        public async Task<ActionResult> Index(CovidCasesViewModel data)
        {
            string dropDownRegion = this.Request.Form.ToString().Remove(0,8);
            CovidCasesViewModel mymodel = new CovidCasesViewModel();
            
            
            
            if (dropDownRegion == "NoRegion")
            {
                mymodel.columnname = "REGION";
                mymodel.reports = await GetReports();

            }
            else
            {
                mymodel.columnname = "PROVINCE";
                mymodel.reports = await GetReports(dropDownRegion);
            }
            
            mymodel.regionslists = await DropdownbindingListAsync();

            return View(mymodel);

        }

        //DropDown
        public async Task<List<SelectListItem>> DropdownbindingListAsync()
        {
            List<SelectListItem> List = new List<SelectListItem>();
            List<regions> Data = await GetRegionslists();
            int cont = 0;
            foreach (var x in Data)
            {
                if (cont == 0)
                {
                    List.Add(new SelectListItem()
                    {
                        Text = "Regions",
                        Value = "NoRegion"
                    });
                    cont++;
                }
                else { 
                    List.Add(new SelectListItem()
                    {
                        Text = x.name,
                        Value = x.iso
                    });
                }
            }
            return List;
        }

        public async Task<List<regions>> GetRegionslists()
        {
            regionslists regionsInfo = new regionslists();
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
                var regionResponse = await response.Content.ReadAsStringAsync();
                regionsInfo = JsonConvert.DeserializeObject<regionslists>(regionResponse);
            }
            return regionsInfo.data;
        }

        public async Task<reportSumlist> GetReports()
        {
            
            reportslists reportsInfo = new reportslists();
            var list = new reportSumlist();
            var internList = new List<reportSum>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Baseurl + "reports"),
                Headers =
                {
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                    { "x-rapidapi-key", "c0aaafc395msh4d87d9a02b6e4bdp18a932jsn6866402cd398" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var reportsResponse = await response.Content.ReadAsStringAsync();
                reportsInfo = JsonConvert.DeserializeObject<reportslists>(reportsResponse);
                var totalCases = from d in reportsInfo.data
                                 group d by new { d.region.iso, d.region.name, d.date } into totals
                                 
                                 select new
                                 {
                                     date = totals.Key.date,
                                     region = totals.Key.name,
                                     confirmed = totals.Sum(c => c.confirmed),
                                     deaths = totals.Sum(c => c.deaths),
                                     recovered = totals.Sum(c => c.recovered)
                                 };
                totalCases = totalCases.OrderByDescending(d => d.confirmed);                 
                int cont = 0;
                foreach (var register in totalCases)
                {
                    cont++;
                    internList.Add(new reportSum { date = register.date, region = register.region, confirmed = register.confirmed, deaths = register.deaths, recovered = register.recovered });
                    if (cont == 10) { break; } 
                }
                list.data = internList;
            }
            

            return list;
        }
        public async Task<reportSumlist> GetReports(string callMethod)
        {
            string urlParameter = "reports?iso=" + callMethod;
            reportslists reportsInfo = new reportslists();
            var list = new reportSumlist();
            var internList = new List<reportSum>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Baseurl + urlParameter),
                Headers =
                {
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                    { "x-rapidapi-key", "c0aaafc395msh4d87d9a02b6e4bdp18a932jsn6866402cd398" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var reportsResponse = await response.Content.ReadAsStringAsync();
                reportsInfo = JsonConvert.DeserializeObject<reportslists>(reportsResponse);
                var totalCases = from d in reportsInfo.data
                                 group d by new { d.region.iso, d.region.province, d.date } into totals

                                 select new
                                 {
                                     date = totals.Key.date,
                                     region = totals.Key.province,
                                     confirmed = totals.Sum(c => c.confirmed),
                                     deaths = totals.Sum(c => c.deaths),
                                     recovered = totals.Sum(c => c.recovered)
                                 };
                totalCases = totalCases.OrderByDescending(d => d.confirmed);
                int cont = 0;
                foreach (var register in totalCases)
                {
                    cont++;
                    internList.Add(new reportSum { date = register.date, region = register.region, confirmed = register.confirmed, deaths = register.deaths, recovered = register.recovered });
                    if (cont == 10) { break; }
                }
                list.data = internList;
            }


            return list;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}