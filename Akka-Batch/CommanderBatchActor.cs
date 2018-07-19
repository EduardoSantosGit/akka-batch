using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public class CommanderBatchActor : ReceiveActor
    {

        public CommanderBatchActor()
        {

        }

        protected override void PreStart()
        {
            base.PreStart();
        }

    }
}
