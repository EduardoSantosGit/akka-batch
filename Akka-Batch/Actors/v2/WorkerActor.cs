using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors.v2
{
    public class WorkerActor : ReceiveActor
    {
        public WorkerActor()
        {

        }

        protected override void PreStart()
        {
            base.PreStart();
        }

    }
}
