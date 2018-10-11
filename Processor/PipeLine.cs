using Gridsum.DataflowEx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Processor
{
    public class PipeLine : Dataflow<string>
    {
        private ExecutionDataflowBlockOptions _options;
        public PipeLine(
            DataflowOptions dataflowOptions,
            int maxDegreeOfParallelism) 
            : base(dataflowOptions)
        {
            _options = dataflowOptions.ToExecutionBlockOption(true);
            _options.MaxDegreeOfParallelism = maxDegreeOfParallelism;
            _options.BoundedCapacity = 10000;
            _options.MaxMessagesPerTask = 100;
            _options.SingleProducerConstrained = true;
        }

        public void BatchRequest(List<string> items)
        {



        }  


        public override ITargetBlock<string> InputBlock => throw new NotImplementedException();
    }
}
