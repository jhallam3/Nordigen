﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Nordigen.Utilities
{
    public class HttpRequestor
    {
        public async Task<T> HttpRestRequester<T>(Method method, [NotNull] string requestUrl, [NotNull] string uri, [NotNull] string accessToken, List<RequestParameter> parameters = null, Object body = null)
        {
            var client = new RestClient(requestUrl + uri)
            {
                Timeout = -1
            };

            var request = new RestRequest(method);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Token " + accessToken);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value, parameter.Type);
                }
            }

            if (body != null)
            {
                request.AddJsonBody(body);
            }

            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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