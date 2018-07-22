using Akka.Actor;
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

            Receive<MessageOneData>(msg => 
            {
                Console.WriteLine(msg);
            });

        }

    }
}
