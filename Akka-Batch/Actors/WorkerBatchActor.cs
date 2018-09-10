using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public class WorkerBatchActor : ReceiveActor
    {
        private RequestService _service;

        public WorkerBatchActor()
        {
            _service = new RequestService();
            Sending();
        }

        public void Sending()
        {
            Receive<MessageOneItem>(msg => 
            {
                //var e = _service.GetPage(msg.LineData).GetAwaiter().GetResult();
                //Console.WriteLine("Result {0}", e);
                Sender.Tell(new MessageSuccess {  Status = "OK", Message = "Success", Sender = msg.Sender });
            });

        }

    }
}
