using System;

namespace Processor
{
    public class RequestClient
    {

        public Result<string> GetDataApi(string url)
        {
            if (string.IsNullOrEmpty(url))
                return new Result<string>(ResultCode.BadRequest, "Param is null or empty");

            


            return new Result<string>(ResultCode.OK);
        }

    }
}
