using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace outdoor.rocks.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = GetTestDataFromRESTTestControllers();
            Console.WriteLine(res.Result);
        }

        private static async Task<string> GetTestDataFromRESTTestControllers()
        {
            var client = new HttpClient();
            var res = await client.GetAsync("http://localhost:50374/api/Test");
            return await res.Content.ReadAsStringAsync();
        }
    }
}
