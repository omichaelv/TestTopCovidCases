using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTopCovidCases.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTopCovidCases.Models;

namespace TestTopCovidCases.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void DropdownbindingListAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRegionslistsTest()
        {
            //arrange

            regionslists regionsInfos = new regionslists();
            regionsInfos.data = regionsInfo();
            Assert.Fail();
        }
       public List<regions> regionsInfo()
        {
            var testregion = new List<regions>();
            testregion.Add(new regions { iso = "US", name = "USA" });
            testregion.Add(new regions { iso = "FR", name = "FRANCE" });

            return testregion;
        }
    }
}