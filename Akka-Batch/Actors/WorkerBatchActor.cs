using Akka.Actor;
using Akka.Batch.Messages;
using Gridsum.DataflowEx;
using Processor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Akka.Batch
{
    public class WorkerBatchActor : ReceiveActor
    {
        private HttpClient _client;
        private PipeLine _pipe;

        public WorkerBatchActor()
        {
            _client = new HttpClient();
            _pipe = new PipeLine(new DataflowOptions
            {
                MonitorInterval = TimeSpan.FromSeconds(2),
                PerformanceMonitorMode = DataflowOptions.PerformanceLogMode.Verbose,
                //FlowMonitorEnabled = false,
                //BlockMonitorEnabled = false,
                RecommendedCapacity = 10000,
                RecommendedParallelismIfMultiThreaded = 64,
            }, 10);
            Sending();
        }

        public void Sending()
        {
            ReceiveAsync<MessageItem>(async msg => 
            {
                await _pipe.Start(msg.Batch);
            });

        }

    }
}
