using System;

namespace Processor
{
    public class RequestClient
    {

        private readonly ProviderHttpMethod _provider;

        public RequestClient(ProviderHttpMethod provider)
        {
            _provider = provider;
        }

        public Result<string> GetDataApi(string url)
        {
            if (string.IsNullOrEmpty(url))
                return new Result<string>(ResultCode.BadRequest, "Param is null or empty");

            var ret = _provider.GetAsync(url).Result;
           
            return ret;
        }

    }
}
