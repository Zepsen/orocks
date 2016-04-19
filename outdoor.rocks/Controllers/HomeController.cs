using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace outdoor.rocks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            string con = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            MongoClient client = new MongoClient(con);
            var list = GetDatabaseNames(client);
            ViewBag.List = list;
            return View();
        }

        private List<string> GetDatabaseNames(MongoClient client)
        {
            List<string> list = new List<string>();
            using (var cursor = client.ListDatabases())
            {
                var databaseDocuments = cursor.ToList();
                foreach (var databaseDocument in databaseDocuments)
                {
                    list.Add(databaseDocument["name"].ToString());                    
                }                
            }

            return list;
        }
    }
}
