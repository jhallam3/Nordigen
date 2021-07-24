using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nordigen.DataTypes;
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

        public async Task<AgreementResponse> CreateAgreement(AgreementRequest agreementRequest, string access_token)
        {
            var bodyparameter = new RequestParameter
            {
                Key = "accept",
                Value = "application/json",
                Type = ParameterType.HttpHeader
            };

            var ListBodyParameters = new List<RequestParameter>();
            ListBodyParameters.Add(bodyparameter);
           
            
            return await new HttpRequestor().HttpRestRequester<AgreementResponse>(Method.POST, baseNordigen, "agreements/enduser/", access_token, ListBodyParameters, agreementRequest);

        }
        
        public async Task<RequisitionResponse> CreateRequisition(RequisitionRequest requisitionRequest, string access_token)
        {
            var bodyparameter = new RequestParameter
            {
                Key = "accept",
                Value = "application/json",
                Type = ParameterType.HttpHeader
            };

            var ListBodyParameters = new List<RequestParameter>();
            ListBodyParameters.Add(bodyparameter);
           
            
            return await new HttpRequestor().HttpRestRequester<RequisitionResponse>(Method.POST, baseNordigen, "requisitions/", access_token, ListBodyParameters, requisitionRequest);

        }
        
        public async Task<RequisitionResponse> CreateLink(RequisitionResponse requisitionResponse, Banks bank, string access_token)
        {
            var bodyparameter = new RequestParameter
            {
                Key = "accept",
                Value = "application/json",
                Type = ParameterType.HttpHeader
            };

            var ListBodyParameters = new List<RequestParameter>();
            ListBodyParameters.Add(bodyparameter);
           
            var result = await new HttpRequestor().HttpRestRequester<RequisitionResponse>(Method.POST, baseNordigen,
                "requisitions/" + requisitionResponse.id + "/links/", access_token, ListBodyParameters,
                "\"{\"aspsp_id\":" + bank.id + "}");
            return result;

        }
        
    }
}