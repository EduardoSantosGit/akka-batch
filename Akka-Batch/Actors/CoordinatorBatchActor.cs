using Akka.Actor;
using Akka.Batch.Actors;
using Akka.Batch.Messages;
using Akka.Routing;
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
                var actorName = $"{ActorPath.Worker.Name}-{pointer}-{count}";
                var actorWorker = Context.Child(actorName);
                if (actorWorker.Equals(ActorRefs.Nobody))
                {
                    actorWorker = Context.ActorOf(Props.Create(() =>
                            new WorkerBatchActor(_pipeline)), actorName);
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
            var e = Context.ActorOf(Props.Create(() => new WorkerPoolActor())
                    .WithDispatcher("custom-task-dispatcher")
                    .WithDeploy(new Deploy(
                     new SmallestMailboxPool(100 ,new DefaultResizer(100, 100), SupervisorStrategy(), "custom-task-dispatcher")
                 )), ActorPath.WorkerPool.Name);
            base.PreStart();
        }

    }
}
