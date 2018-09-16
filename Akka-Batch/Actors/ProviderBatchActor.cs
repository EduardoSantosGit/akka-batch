using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors
{
    public class ProviderBatchActor : ReceiveActor
    {
        private string _path;
        public IActorRef _commanderActor;

        public ProviderBatchActor(string path)
        {
            _path = path;

            Reading();
        }

        public void Reading()
        {



        }

        protected override void PreStart()
        {
            _commanderActor = Context.ActorOf(Props.Create(() =>
                new CommanderBatchActor()), ActorPath.Commander.Name);

            base.PreStart();
        }

    }
}
