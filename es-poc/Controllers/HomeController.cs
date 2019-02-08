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
            //x this.Init();
            //MongoDbClient client = new MongoDbClient();
            //var foo = client.Query("5c5c91bfc57088c64c04918b");

            //return new ContentResult()
            //{
            //    Content = foo.AsString,
            //    StatusCode = 200


            //};
            // ViewBag["Message"] = foo.AsString;
            // ViewBag["Other"] = "It's working";
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
