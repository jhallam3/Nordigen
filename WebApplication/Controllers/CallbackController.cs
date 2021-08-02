using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CallbackController : Controller
    {
        private readonly ILogger<CallbackController> _logger;

        public CallbackController(ILogger<CallbackController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string token = System.IO.File.ReadAllText("/Users/jonathanhallam/Documents/Nordigen/Token"); 
            
            
            var requisition = Request.Query["ref"];
            var ListAccounts = new Nordigen.Utilities.HttpCall().ListAccounts(requisition, token).Result;

            foreach (var v in ListAccounts.accounts)
            {
                var listtransactions = new Nordigen.Utilities.HttpCall().ListTransactions(v, token).Result;
            
                var listdetails = new Nordigen.Utilities.HttpCall().Listdetails(v, token).Result;
                var listbalances = new Nordigen.Utilities.HttpCall().ListBalances(v, token).Result;

            }
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}