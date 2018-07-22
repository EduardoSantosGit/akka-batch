using Akka.Actor;
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

            Receive<MessageOneData>(msg => 
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
