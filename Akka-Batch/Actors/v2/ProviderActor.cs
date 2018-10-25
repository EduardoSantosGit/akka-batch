using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors.v2
{
    public class ProviderActor : ReceiveActor
    {

        private IActorRef _commanderActor;

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
            _commanderActor = Context.ActorOf(Props.Create(() =>
                new CommanderActor()), ActorPath.Commander.Name);

            base.PreStart();
        }

    }
}
