using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Akka.Batch.Actors
{
    public class WorkerPoolActor : ReceiveActor
    {

        private IActorRef _actorRef;

        public WorkerPoolActor()
        {
            Console.WriteLine("Criou");

            Sending();
        }

        public void Sending()
        {
            Receive<MessageItem>(msg =>
            {
                var point = msg.Message.RefPointer.ToString();
                var count = msg.Message.CountBatch.ToString();
                //Console.WriteLine("Actor Sending Pointer {0} Count {1}", point, count);

                _actorRef.Tell(msg);

                Thread.Sleep(20000);

                //Become(Working);
            });
        }

        //public void Working()
        //{
        //    Receive<MessageItem>(msg =>
        //    {
        //        var point = msg.Message.RefPointer.ToString();
        //        var count = msg.Message.CountBatch.ToString();
        //        Console.WriteLine("Actor Working Pointer {0} Count {1}", point, count);
        //    });
        //}
        protected override void PreStart()
        {
            _actorRef = Context.ActorOf(Props.Create(() => new SimulatorActor())
                   , ActorPath.WorkerPool.Name);
            base.PreStart();
        }
    }
}
