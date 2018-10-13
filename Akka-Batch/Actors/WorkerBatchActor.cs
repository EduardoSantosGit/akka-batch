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
        private PipeLine _pipeline;

        public WorkerBatchActor(PipeLine pipeline)
        {
            _client = new HttpClient();
            _pipeline = pipeline;

            Sending();
        }

        public void Sending()
        {
            ReceiveAsync<MessageItem>(async msg => 
            {
                await _pipeline.Start(msg.Batch);
            });

        }

    }
}
