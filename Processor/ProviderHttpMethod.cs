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

        public async Task<Result<string>> GetAsync(string url)
        {
            var response = await _client.GetAsync(url);
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return new Result<string>(ResultCode.OK, await response.Content.ReadAsStringAsync());
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return new Result<string>(ResultCode.BadRequest, await response.Content.ReadAsStringAsync());
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new Result<string>(ResultCode.NotFound, await response.Content.ReadAsStringAsync());

            return new Result<string>(ResultCode.Error);
        }

    }
}
