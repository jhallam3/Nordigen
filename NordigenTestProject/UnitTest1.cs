using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nordigen.DataTypes;
using Nordigen.DataTypes.Agreements;
using Nordigen.DataTypes.ASPSPS;
using Nordigen.DataTypes.Requisitions;
using NUnit.Framework;

namespace NordigenTestProject
{
    public class Tests
    {
        private string token = System.IO.File.ReadAllText("/Users/jonathanhallam/Documents/Nordigen/Token");
        private string UserID = Guid.NewGuid().ToString();
        private string RequistionID = Guid.NewGuid().ToString();
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task<List<Banks>> GetAllBanks()
        {
            //var env = System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
            var banks =
                await new Nordigen.Utilities.HttpCall().GetBanks("GB", token);
            //if(banks != null)
            ///{Assert.Pass();}

            return banks;
        }
        [Test]
        public async Task<AgreementResponse> CreateAgreement()
        {
            var banks = await GetAllBanks();

            var AgreementRequest = new AgreementRequest()
            {
                aspsp_id = banks.ElementAt(0).id, enduser_id = UserID, max_historical_days = banks.ElementAt(0).transaction_total_days
            };

            var CheckHTTPCLient =
                await new Nordigen.Utilities.HttpCall().CreateAgreement(AgreementRequest, token);
            //if(CheckHTTPCLient != null)
            //{Assert.Pass();}
            return CheckHTTPCLient;
        }
        
        [Test]
        public async Task<RequisitionResponse> Createrequisitions()
        {
            var banks = await GetAllBanks();
            var agreement = await CreateAgreement();
            
            var RequisitionRequest = new RequisitionRequest
            {
                redirect = "http://localhost",
                reference = RequistionID,
                enduser_id = UserID,
                agreements = new List<string>(){ agreement.id },
                user_language = "en"
            };

            var CheckHTTPCLient =
                await new Nordigen.Utilities.HttpCall().CreateRequisition(RequisitionRequest, token);
           // if(CheckHTTPCLient != null)
           // {Assert.Pass();}
           return CheckHTTPCLient;
        }
        [Test]
        public async Task BuildLink()
        {
            var banks = await GetAllBanks();
            var agreement = await CreateAgreement();
            
            var RequisitionRequest = new RequisitionRequest
            {
                redirect = "http://localhost",
                reference = RequistionID,
                enduser_id = UserID,
                agreements = new List<string>(){ agreement.id },
                user_language = "en"
            };

            var RequisitionResponse = await Createrequisitions();
            
            var CheckHTTPCLient =
                await new Nordigen.Utilities.HttpCall().CreateLink(RequisitionResponse, banks.ElementAt(0), token);
            if(CheckHTTPCLient != null)
            {Assert.Pass();}
        }
        
        
        
    }
}