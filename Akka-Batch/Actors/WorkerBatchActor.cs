using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public class WorkerBatchActor : ReceiveActor
    {

        public WorkerBatchActor()
        {
            Sending();
        }

        public void Sending()
        {
            Receive<MessageOneItem>(msg => 
            {
                Console.WriteLine(msg);
            });

        }

    }
}
