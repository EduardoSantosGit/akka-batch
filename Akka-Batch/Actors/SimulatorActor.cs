using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors
{
    public class SimulatorActor : ReceiveActor
    {
        public SimulatorActor()
        {
            Sending();
        }

        public void Sending()
        {
            Receive<MessageItem>(msg =>
            {
                var point = msg.Message.RefPointer.ToString();
                var count = msg.Message.CountBatch.ToString();
                Console.WriteLine("Simulator Sending Pointer {0} Count {1}", point, count);
            });
        }

    }
}
