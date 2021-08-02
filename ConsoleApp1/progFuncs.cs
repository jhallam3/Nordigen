// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Nordigen.DataTypes;
// using Nordigen.DataTypes.Agreements;
// using Nordigen.DataTypes.ASPSPS;
// using Nordigen.DataTypes.Requisitions;
//
// namespace ConsoleApp1
// {
//     public class progFuncs
//     {
//         
// public static async Task<List<Banks>> GetAllBanks(string token)
//         {
//             //var env = System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
//             var banks =
//                 await new Nordigen.Utilities.HttpCall().GetBanks("GB", token);
//             
//             return banks;
//         }
//         
//         public async Task<AgreementResponse> CreateAgreement(string UserID, Banks bank, string token)
//         {
//
//            
//
//             var result =
//                 await new Nordigen.Utilities.HttpCall().CreateAgreement(agreementRequest, token);
//             
//             return result;
//         }
//         
//         
//         public async Task<RequisitionResponse> Createrequisitions(RequisitionRequest requisitionRequest, string token)
//         {
//             var result =
//                 await new Nordigen.Utilities.HttpCall().CreateRequisition(requisitionRequest, token);
//             return result;
//         }
//         
//         public async Task<LinkResponse> BuildLink(Banks bank, RequisitionResponse requisition, string userID, string agreementID, string token)
//         {
//             var CheckHTTPCLient = await new Nordigen.Utilities.HttpCall().CreateLink(requisition, bank, token);
//
//             Console.WriteLine(requisition.id);
//             
//             return CheckHTTPCLient;
//             
//         }
//
//        
//         public async Task ListAccounts()
//         {
//             var CheckHTTPCLient =
//                 await new Nordigen.Utilities.HttpCall().ListAccounts(null, null);
//
//         }
//         
//     
//     }
// }