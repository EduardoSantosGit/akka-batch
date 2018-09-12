using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    public class ProviderHttpMethod
    {
        private readonly HttpClient _client;

        public ProviderHttpMethod(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var response = await _client.GetAsync(url);

            return response;
        }

    }
}
