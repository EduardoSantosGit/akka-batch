using Akka.Actor;
using Akka.Batch.Messages;
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

            Receive<MessageOneItem>(msg => 
            {
                var actorWorker = Context.Child(msg.LineData);
                if (actorWorker.Equals(ActorRefs.Nobody))
                {
                    actorWorker = Context.ActorOf(Props.Create(() =>
                            new WorkerBatchActor()), msg.LineData);
                }
                msg.Sender = Sender;
                actorWorker.Tell(msg);
            });

            Receive<MessageSuccess>(msg => 
            {
                msg.Sender.Tell(msg);
            });

            Receive<MessageError>(msg => 
            {
                msg.Sender.Tell(msg);
            });

        }

        protected override void PreStart()
        {
            base.PreStart();
        }

    }
}
