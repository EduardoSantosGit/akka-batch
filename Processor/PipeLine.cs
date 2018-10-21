using Gridsum.DataflowEx;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Processor
{
    public class PipeLine : Dataflow<string>
    {
        private ExecutionDataflowBlockOptions _options;
        private DataflowOptions _dataflowOptions;
        private Dataflow<string, HttpResponseMessage> _request;
        private Dataflow<HttpResponseMessage> _flow;
        private TransformBlock<string, HttpResponseMessage> _requestBlock;
        private ActionBlock<HttpResponseMessage> _flowBlock;
        private int _totalOut;

        public PipeLine(
            DataflowOptions dataflowOptions,
            int maxDegreeOfParallelism) 
            : base(dataflowOptions)
        {
            _dataflowOptions = dataflowOptions;
            _options = dataflowOptions.ToExecutionBlockOption(true);
            _options.MaxDegreeOfParallelism = maxDegreeOfParallelism;
            _options.BoundedCapacity = 10000;
            _options.MaxMessagesPerTask = 100;
            _options.SingleProducerConstrained = true;

            _requestBlock = new TransformBlock<string, HttpResponseMessage>(x =>
            {
                Interlocked.Increment(ref _totalOut);

                Console.WriteLine("lines " + _totalOut);

                //var http = new HttpClient();

                //var response = http.GetAsync("http://localhost:3000/endpoint/info").Result;

                return default(HttpResponseMessage);
            }, _options);

            _flowBlock = new ActionBlock<HttpResponseMessage>(x =>
            {

            }, _options);

            _request = _requestBlock.ToDataflow(_dataflowOptions, "Request");
            _flow = _flowBlock.ToDataflow(_dataflowOptions, "Flow");

            _request.LinkTo(_flow);
        }

        public async Task Start(List<string> items)
        {
            await this.ProcessAsync(items, false);
        }

        public override ITargetBlock<string> InputBlock => _request.InputBlock;
    }
}
