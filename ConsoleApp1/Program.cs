using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nordigen.DataTypes;
using Nordigen.DataTypes.Agreements;
using Nordigen.DataTypes.ASPSPS;
using Nordigen.DataTypes.Requisitions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = System.IO.File.ReadAllText("/Users/jonathanhallam/Documents/Nordigen/Token"); 
            string userID = Guid.NewGuid().ToString();
            string requistionID = Guid.NewGuid().ToString();
            Console.WriteLine("hello");
            
            
            //2 get banks
            var country = "GB";
            var bankName = "Lloyds Bank Personal";
            var banks = new Nordigen.Utilities.HttpCall().GetBanks(country, token).Result;
//
            var bank = banks.Where(x => x.name.Contains(bankName)).First();
            //var bank = banks.Where(x => x.name.Contains("sand")).First();

            bank.id = "SANDBOXFINANCE_SFIN0000";
            
            //Step 3: Create an end-user agreement
            
            var agreementRequest = new AgreementRequest()
            {
                aspsp_id = bank.id, 
                enduser_id = userID, 
                max_historical_days = "30"
            };

            var agreement = new Nordigen.Utilities.HttpCall().CreateAgreement(agreementRequest, token, true, true, true);
            
            //4 Step 4.1: Create a requisition

            var requisitionrequest = new RequisitionRequest
            {
                enduser_id = userID,
                reference = requistionID,
                redirect = "https://localhost:5001/callback",
                agreements = new List<string> {agreement.Result.id},
                user_language = "EN"
            };


            var requisition = new Nordigen.Utilities.HttpCall().CreateRequisition(requisitionrequest, token).Result;
            
            //5 Build a Link

            var link = new Nordigen.Utilities.HttpCall().CreateLink(requisition, bank, token).Result;
            
            Console.WriteLine("link to authenticate to your bank - ");
            Console.WriteLine(link.initiate);
            
            //6 List accounts

            var ListAccounts = new Nordigen.Utilities.HttpCall().ListAccounts(requisition.id, token).Result;

            foreach (var v in ListAccounts.accounts)
            {
                var listtransactions = new Nordigen.Utilities.HttpCall().ListTransactions(v, token).Result;
            
                var listdetails = new Nordigen.Utilities.HttpCall().Listdetails(v, token).Result;
                var listbalances = new Nordigen.Utilities.HttpCall().ListBalances(v, token).Result;

            }
            
            
        }
    }
}

