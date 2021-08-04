using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Nordigen.Utilities
{
    internal class HttpRequestor
    {
        public async Task<T> HttpRestRequester<T>(Method method, [NotNull] string requestUrl, [NotNull] string uri,
            [NotNull] string accessToken, List<RequestParameter> parameters = null, object body = null)
        {
            var client = new RestClient(requestUrl + uri)
            {
                Timeout = -1
            };

            var request = new RestRequest(method);

            if (uri == "requisitions/" || uri.EndsWith("/links/"))
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            else
                request.AddHeader("Content-Type", "application/json");

            request.AddHeader("Authorization", "Token " + accessToken);
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            if (parameters != null)
                foreach (var parameter in parameters)
                    request.AddParameter(parameter.Key, parameter.Value);

            if (body != null) request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //throw new UserFriendlyException("Not Authenticated to Revolut Please Re-authenticate");
            }

            if (response.StatusDescription == "Unprocessable Entity")
            {
                //throw new UserFriendlyException("Unprocessable", null, response.Content);
            }

            if (response.StatusDescription == "Bad Request")
            {
                //throw new UserFriendlyException("Bad Request", null, response.Content);
            }

            if (response.StatusDescription == "Server Error")
            {
                //throw new UserFriendlyException("Server Error", null, response.Content);
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}