using System.Diagnostics;
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
            var token = System.IO.File.ReadAllText("/Users/jonathanhallam/Documents/Nordigen/Token");


            var requisition = Request.Query["ref"];
            var ListAccounts = new Nordigen.Nordigen().ListAccounts(requisition, token).Result;

            foreach (var v in ListAccounts.accounts)
            {
                var listtransactions = new Nordigen.Nordigen().ListTransactions(v, token).Result;

                var listdetails = new Nordigen.Nordigen().Listdetails(v, token).Result;
                var listbalances = new Nordigen.Nordigen().ListBalances(v, token).Result;
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