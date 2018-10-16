using Akka.Actor;
using Akka.Batch.Messages;
using Processor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public class CoordinatorBatchActor : ReceiveActor
    {
        private IActorRef _actorRef;
        private PipeLine _pipeline;

        public CoordinatorBatchActor(PipeLine pipeline)
        {
            _pipeline = pipeline;

            Processing();
        }

        public void Processing()
        {

            Receive<MessageItem>(msg => 
            {
                var pointer = msg.Message.RefPointer.ToString();
                var count = msg.Message.CountBatch.ToString();
                var actorWorker = Context.Child($"{pointer}-{count}");
                if (actorWorker.Equals(ActorRefs.Nobody))
                {
                    actorWorker = Context.ActorOf(Props.Create(() =>
                            new WorkerBatchActor(_pipeline)), 
                            $"{ActorPath.Worker.Name}-{pointer}-{count}");
                }
                msg.RefSender = Sender;
                actorWorker.Tell(msg);
            });

            Receive<MessageSuccess>(msg => 
            {
                msg.RefSender.Tell(msg);
            });

            Receive<MessageError>(msg => 
            {
                msg.RefSender.Tell(msg);
            });

        }

        protected override void PreStart()
        {
            base.PreStart();
        }

    }
}
