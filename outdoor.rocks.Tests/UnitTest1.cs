using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using outdoor.rocks.Controllers;
using System.Timers;
using System.Collections.Generic;

namespace outdoor.rocks.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TrailsController controller = new TrailsController();

            // Действие
            var timer = new Timer();
            timer.Start();

            IEnumerable<string> result = controller.Get();

            timer.Stop();
            Console.WriteLine("Time: " + timer.ToString());
            // Утверждение
        }
    }
}
