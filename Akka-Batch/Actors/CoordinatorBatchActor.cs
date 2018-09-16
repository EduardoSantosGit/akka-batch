using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public class CoordinatorBatchActor : ReceiveActor
    {
        private IActorRef _actorRef;

        public CoordinatorBatchActor()
        {
            Processing();
        }

        public void Processing()
        {

            Receive<MessageItem>(msg => 
            {
                var actorWorker = Context.Child(msg.Body);
                if (actorWorker.Equals(ActorRefs.Nobody))
                {
                    actorWorker = Context.ActorOf(Props.Create(() =>
                            new WorkerBatchActor()), msg.Body);
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
