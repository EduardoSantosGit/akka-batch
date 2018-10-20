using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors
{
    public class WorkerPoolActor : ReceiveActor
    {

        public WorkerPoolActor()
        {
            Console.WriteLine("Criou");
        }

    }
}
