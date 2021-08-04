using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nordigen.DataTypes;
using Nordigen.DataTypes.Agreements;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var token = File.ReadAllText("/Users/jonathanhallam/Documents/Nordigen/Token");
            var userID = Guid.NewGuid().ToString();
            var requistionID = Guid.NewGuid().ToString();
            Console.WriteLine("hello");


            //2 get banks
            var country = "GB";
            var bankName = "Lloyds Bank Personal";
            var banks = new Nordigen.Nordigen().GetBanks(country, token).Result;
//
            var bank = banks.Where(x => x.name.Contains(bankName)).First();
            //var bank = banks.Where(x => x.name.Contains("sand")).First();

            bank.id = "SANDBOXFINANCE_SFIN0000";

            //Step 3: Create an end-user agreement

            var agreementRequest = new AgreementRequest
            {
                aspsp_id = bank.id,
                enduser_id = userID,
                max_historical_days = "30"
            };

            var agreement = new Nordigen.Nordigen().CreateAgreement(agreementRequest, token, true, true, true);

            //4 Step 4.1: Create a requisition

            var requisitionrequest = new RequisitionRequest
            {
                enduser_id = userID,
                reference = requistionID,
                redirect = "https://localhost:5001/callback",
                agreements = new List<string> {agreement.Result.id},
                user_language = "EN"
            };


            var requisition = new Nordigen.Nordigen().CreateRequisition(requisitionrequest, token).Result;

            //5 Build a Link

            var link = new Nordigen.Nordigen().CreateLink(requisition, bank, token).Result;

            Console.WriteLine("link to authenticate to your bank - ");
            Console.WriteLine(link.initiate);

            //6 List accounts

            var ListAccounts = new Nordigen.Nordigen().ListAccounts(requisition.id, token).Result;

            foreach (var v in ListAccounts.accounts)
            {
                var listtransactions = new Nordigen.Nordigen().ListTransactions(v, token).Result;

                var listdetails = new Nordigen.Nordigen().Listdetails(v, token).Result;
                var listbalances = new Nordigen.Nordigen().ListBalances(v, token).Result;
            }
        }
    }
}