using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            Receive<MessageReader>(msg =>
            {
                var lines = File.ReadLines(_path).AsParallel()
                                                 .Skip(msg.RefPointer)
                                                 .Take(msg.CountBatch)
                                                 .ToList();

                msg.RefPointer = msg.RefPointer + msg.CountBatch;

                var message = new MessageItem
                {
                    Batch = lines,
                    RefSender = Self,
                    Message = msg
                };
                _commanderActor.Tell(message);
            });
        }

        protected override void PreStart()
        {
            _commanderActor = Context.ActorOf(Props.Create(() =>
                new CommanderBatchActor()), ActorPath.Commander.Name);

            base.PreStart();
        }

    }
}
