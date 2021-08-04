# Nordigen
Nordigen OpenBanking c#

check out the console application. 

this has example code to run. 

example below. 

```c#
            var token = File.ReadAllText("/Users/jonathanhallam/Documents/Nordigen/Token");
            var userID = Guid.NewGuid().ToString();
            var requistionID = Guid.NewGuid().ToString();
            var country = "GB";
            //example UK bank.
            var bankName = "Lloyds Bank Personal";
            var banks = new Nordigen.Nordigen().GetBanks(country, token).Result;

            var bank = banks.Where(x => x.name.Contains(bankName)).First();
            
            // setting this sets the sandbox bank
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

            Console.WriteLine("link to authenticate to your bank - any username or pin to login ");
            Console.WriteLine(link.initiate);

            Console.WriteLine("");
            Console.WriteLine("connect to the bank above and then press enter");
            Console.ReadKey();
            //6 List accounts

            var ListAccounts = new Nordigen.Nordigen().ListAccounts(requisition.id, token).Result;

            Console.WriteLine("--- Accounts ---");
            Console.WriteLine(JsonConvert.SerializeObject(ListAccounts));
            Console.WriteLine("");
            foreach (var v in ListAccounts.accounts)
            {
                var listtransactions = new Nordigen.Nordigen().ListTransactions(v, token).Result;

                Console.WriteLine("--- Transactions ---");
                Console.WriteLine(JsonConvert.SerializeObject(listtransactions));
                Console.WriteLine("");
                
                var listdetails = new Nordigen.Nordigen().Listdetails(v, token).Result;
                
                Console.WriteLine("--- Details ---");
                Console.WriteLine(JsonConvert.SerializeObject(listdetails));
                Console.WriteLine("");
                
                var listbalances = new Nordigen.Nordigen().ListBalances(v, token).Result;
                
                Console.WriteLine("--- Balances ---");
                Console.WriteLine(JsonConvert.SerializeObject(listbalances));
                Console.WriteLine("");
            }
```