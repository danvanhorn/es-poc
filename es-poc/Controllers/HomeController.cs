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
using es_poc.Dal.ElasticSearch;
using es_poc.Dal.Entities;

namespace es_poc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            SearchModel model = new SearchModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Init() {
            MongoDbClient dbClient = new MongoDbClient();
            ElasticSearchClient esClient = new ElasticSearchClient();
            dbClient.Init();
            esClient.Index();
            return new ContentResult() {
                Content = "OK"
            };
        }

        public IActionResult Search(SearchModel search)
        {
            if (search.query != null)
            {
                ElasticSearchClient esClient = new ElasticSearchClient();
                search.models = esClient.Search(search.query).Select(x => (SearchResultViewModel)x).ToList();
            }
            return View("Index", search);
        }

        public IActionResult Details(string id) {
            MongoDbClient dbClient = new MongoDbClient();
     
            var result = dbClient.Query(id)
                .Select(x => (SearchDetailsViewModel)x)
                .ToList();

            return View("SearchDetailsViewModel", result.FirstOrDefault());
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
