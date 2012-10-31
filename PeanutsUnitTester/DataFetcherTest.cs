using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Peanuts.Helpers;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PeanutsUnitTester
{
    [TestClass]
    public class DataFetcherTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Peanuts.Helpers.DataFetcher df = new DataFetcher();
            var taskResult = df.getTVServices();
            taskResult.Wait();
            Dictionary<string, string> result = taskResult.Result;
            Assert.IsNotNull(result);      
        }
    }
}
