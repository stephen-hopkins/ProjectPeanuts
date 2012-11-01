using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using System.Diagnostics;
using Peanuts;

namespace PeanutsUnitTester
{
    [TestClass]
    public class DataFetcherTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataFetcher df = new DataFetcher();
            var taskResult = df.getTVServices();
            taskResult.Wait();
            TVServiceCollection result = taskResult.Result;
            Assert.IsNotNull(result);      
        }
    }
}
