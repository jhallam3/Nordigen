using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nordigen.DataTypes;
using Nordigen.DataTypes.Accounts;
using Nordigen.DataTypes.Agreements;
using Nordigen.DataTypes.ASPSPS;
using Nordigen.DataTypes.Requisitions;
using RestSharp;

namespace Nordigen.Utilities
{
    public class HttpCall
    {

        private const string baseNordigen = "https://ob.nordigen.com/api/";

        public async Task<List<Banks>> GetBanks (string country, string access_token)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try	
            {//var accounts = await new Accountz.RestCalls.HttpRequestor().HttpRestRequester<List<RevolutDataTypes.MyArray>>(Method.GET, tenantsettings.ExtraProperties.GetOrDefault(MultiTenancyConsts.RevolutRequestURLPropertyName).ToString(), "/accounts", access_token);

                var accounts = await new HttpRequestor().HttpRestRequester<List<DataTypes.ASPSPS.Banks>>(
                    Method.GET, baseNordigen, "aspsps/?country=gb", access_token);
                return accounts;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
                return null;
            }
        }

        public async Task<AgreementResponse> CreateAgreement(AgreementRequest agreementRequest, string access_token, bool balances, bool details, bool transactions  )
        {
            //{{baseUrl}}/api/agreements/enduser/
            
            var bodyparameter = new RequestParameter
            {
                Key = "accept",
                Value = "application/json",
                Type = ParameterType.HttpHeader
            };

            var ListBodyParameters = new List<RequestParameter>();
            
            ListBodyParameters.Add(bodyparameter);


            if (balances == true)
            {
                ListBodyParameters.Add((new RequestParameter()
                {
                    Key = "access_scope", 
                    Value =  "balances"
                }));
            }
            
            if (details == true)
            {
                ListBodyParameters.Add((new RequestParameter()
                {
                    Key = "access_scope", 
                    Value =  "details"
                }));
            }
            
            if (transactions == true)
            {
                ListBodyParameters.Add((new RequestParameter()
                {
                    Key = "access_scope", 
                    Value =  "transactions"
                }));
            }
            
            return await new HttpRequestor().HttpRestRequester<AgreementResponse>(Method.POST, baseNordigen, "agreements/enduser/", access_token, ListBodyParameters, agreementRequest);

        }
        
        public async Task<RequisitionResponse> CreateRequisition(RequisitionRequest requisitionRequest, string access_token)
        {
            
            //{{baseUrl}}/api/requisitions/
            
            

            var ListBodyParameters = new List<RequestParameter>();
                
            ListBodyParameters.Add((new RequestParameter()
            {
                Key = "enduser_id", Value =  requisitionRequest.enduser_id
            }));
           
            ListBodyParameters.Add((new RequestParameter()
            {
                Key = "reference", Value =  requisitionRequest.reference
            }));
            
            ListBodyParameters.Add((new RequestParameter()
            {
                Key = "redirect", Value =  requisitionRequest.redirect
            }));

            
            ListBodyParameters.Add((new RequestParameter()
            {
                Key = "user_language", Value =  requisitionRequest.user_language
            }));

            foreach (var agreement in requisitionRequest.agreements)
            {
                ListBodyParameters.Add((new RequestParameter()
                {
                    Key = "agreements", Value =  agreement
                }));
            }
            
            return await new HttpRequestor().HttpRestRequester<RequisitionResponse>(Method.POST, baseNordigen, "requisitions/", access_token, ListBodyParameters,null);

        }
        
        public async Task<LinkResponse> CreateLink(RequisitionResponse requisitionResponse, Banks bank, string access_token)
        {
            
            var ListBodyParameters = new List<RequestParameter>();
            ListBodyParameters.Add(new RequestParameter()
            {
                Key = "aspsp_id", 
                Value = bank.id
            });
            
            var result = await new HttpRequestor().HttpRestRequester<LinkResponse>(Method.POST, baseNordigen,
                "requisitions/" + requisitionResponse.id + "/links/", access_token, ListBodyParameters,null
                );
               
            return result;

        }


        public async Task<AccountList> ListAccounts(string requisitionID, string access_token)
        {
            var result = await new HttpRequestor().HttpRestRequester<AccountList>(Method.GET, baseNordigen,
                "requisitions/" + requisitionID + "/", access_token, null,null
            );
            return result;
        }
        
        
        public async Task<AccountsTransactions> ListTransactions(string accountID, string access_token)
        {
            
            var result = await new HttpRequestor().HttpRestRequester<AccountsTransactions>(Method.GET, baseNordigen,
                 "accounts/" + accountID + "/transactions/", access_token, null,null
            );
               
            return result;

        }
        
        public async Task<AccountBalances> ListBalances(string accountID, string access_token)
        {
            
            var result = await new HttpRequestor().HttpRestRequester<AccountBalances>(Method.GET, baseNordigen,
                "accounts/" + accountID + "/balances/", access_token, null,null
            );
               
            return result;

        }
        
        public async Task<AccountDetails> Listdetails(string accountID, string access_token)
        {
            
            var result = await new HttpRequestor().HttpRestRequester<AccountDetails>(Method.GET, baseNordigen,
                "accounts/" + accountID + "/details/", access_token, null,null
            );
               
            return result;

        }
    }
}