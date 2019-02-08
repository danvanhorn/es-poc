using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using es_poc.Models;
using es_poc.Dal.Mongo;
using MongoDB.Driver;
using MongoDB.Bson;

namespace es_poc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Init() {
            MongoDbClient client = new MongoDbClient();
            client.Init();

            return new ContentResult() {
                Content = "OK"
            };
        }

        public IActionResult Query(string id)
        {
            MongoDbClient client = new MongoDbClient();
            var result = client.Query(id);

            return new JsonResult(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
