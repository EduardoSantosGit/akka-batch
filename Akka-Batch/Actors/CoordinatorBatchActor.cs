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
                _actorRef.Tell(msg);
            });

        }

        protected override void PreStart()
        {
            _actorRef = Context.ActorOf(Props.Create(() => new WorkerBatchActor())
                 , ActorPath.Worker.Name);

            base.PreStart();
        }

    }
}
