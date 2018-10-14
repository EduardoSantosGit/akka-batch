using Akka.Actor;
using Akka.Batch.Messages;
using Processor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Actors
{
    public class CommanderBatchActor : ReceiveActor
    {
        private IActorRef _coordinator;
        private PipeLine _pipeline;

        public CommanderBatchActor(PipeLine pipeline)
        {
            _pipeline = pipeline;

            Working();
        }

        public void Working()
        {
            Receive<MessageItem>(msg => 
            {
                _coordinator.Tell(msg);

                if (msg.Batch.Count > 0)
                {
                    Sender.Tell(msg.Message);
                }
            });
        }

        protected override void PreStart()
        {
            _coordinator = Context.ActorOf(Props.Create(() =>
                new CoordinatorBatchActor(_pipeline), SupervisorStrategy()), ActorPath.Coordinator.Name);

            base.PreStart();
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    switch (ex)
                    {
                        case NullReferenceException nre:
                            return Directive.Restart;
                        case ArgumentException are:
                            return Directive.Stop;
                        default:
                            return Directive.Escalate;
                    }
                });
        }

    }
}
