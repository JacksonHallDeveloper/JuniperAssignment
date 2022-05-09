using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JuniperAssignment.Services.Taxes
{
    public sealed class TaxJarHttpClient : HttpClient
    {
        private const string UrlBase = "https://api.taxjar.com/v2/";
        private const string ApiKey = "5da2f821eee4035db4771edab942a4cc";

        private TaxJarHttpClient()
        {
            BaseAddress = new Uri(UrlBase, UriKind.Absolute);
            DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
            DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static TaxJarHttpClient Instance { get; } = new();
    }
}