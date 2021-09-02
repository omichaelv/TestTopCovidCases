using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTopCovidCases.ViewModels
{
    public class CovidCasesViewModel
    {
        public List<SelectListItem> regionslists { get; set; }
        public Models.reportSumlist reports { get; set; }

        public string columnname { get; set; }

    }
}