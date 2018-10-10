using Akka.Actor;
using Akka.Batch.Messages;
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

        public WorkerBatchActor()
        {
            _client = new HttpClient();
            Sending();
        }

        public void Sending()
        {
            Receive<MessageItem>(msg => 
            {
                foreach (var item in msg.Batch)
                {
                    //Console.WriteLine("line " + item + "actor " + Self.Path);
                }
                
                //Console.WriteLine("https://httpbin.org/get/" + msg.Body);
                //var r = _client.GetAsync("https://httpbin.org/get/" + msg.Body).Result;
            });

        }

    }
}
