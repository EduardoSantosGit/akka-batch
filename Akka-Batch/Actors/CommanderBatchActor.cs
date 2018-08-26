using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors
{
    public class CommanderBatchActor : ReceiveActor
    {
        private IActorRef _coordinator;

        public CommanderBatchActor()
        {
            Reading();
        }

        public void Reading()
        {
            Receive<MessageOneItem>(msg => 
            {
                _coordinator.Tell(msg);
            });
        }

        protected override void PreStart()
        {
            _coordinator = Context.ActorOf(Props.Create(() =>
                new CoordinatorBatchActor()), ActorPath.Coordinator.Name);

            base.PreStart();
        }

    }
}
