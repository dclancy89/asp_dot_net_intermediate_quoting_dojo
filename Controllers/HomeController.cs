using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//using DbConnection;
//List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM users");

namespace Quoting_Dojo
{
    public class HomeController : Controller 
    {
        private DbConnector cnx;
        public HomeController()
        {
            cnx = new DbConnector();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult ShowQuotes()
        {
            List<Dictionary<string, object>> AllQuotes = cnx.Query("SELECT * FROM quotes ORDER BY created_at DESC");
            ViewBag.Quotes = AllQuotes;
            return View();
        }

        [HttpPost]
        [Route("quotes")]
        public IActionResult SaveQuote(string name, string quote)
        {

            string query = $"INSERT INTO quotes (name, quote, created_at, updated_at) VALUES ('{name}', '{quote}', NOW(), NOW())";
            cnx.Execute(query);
            return RedirectToAction("ShowQuotes");
            
        }
    }
}