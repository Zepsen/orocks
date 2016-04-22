using Microsoft.VisualStudio.TestTools.UnitTesting;
using outdoor.rocks.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace outdoor.rocks.Tests.Controllers
{
    [TestClass]
    class TrailsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Упорядочение
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
