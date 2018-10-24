using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors.v2
{
    public class ProviderActor : ReceiveActor
    {
        public ProviderActor()
        {
            Starting();
        }

        public void Starting()
        {
            Become(Reading);
        }

        public void Reading()
        {

        }

        public void Stopping()
        {
        }

        protected override void PreStart()
        {
            base.PreStart();
        }

    }
}
