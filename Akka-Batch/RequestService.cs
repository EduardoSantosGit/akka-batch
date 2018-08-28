using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Batch
{
    public class RequestService
    {

        private string _baseUrl;
        private HttpClient _httpClient;

        public RequestService()
        {
            _baseUrl = "";
            _httpClient = new HttpClient();
        }

        public async Task<string> GetPage(string item)
        {
            var result = await _httpClient.GetAsync(_baseUrl);
            Console.WriteLine("line {0}", item);
            var ret = await result.Content.ReadAsStringAsync();
            return item;
        }

    }
}
