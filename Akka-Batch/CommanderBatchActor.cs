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

            Processing();

        }

        public void Processing()
        {



        }

        protected override void PreStart()
        {
            base.PreStart();
        }

    }
}
