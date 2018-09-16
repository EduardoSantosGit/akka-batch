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
            Receive<string>(msg => msg == "Start", m =>
            {
               File
                .ReadAllLines(_path)
                .AsParallel()
                .ToList()
                .ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
                    {
                        var message = new MessageItem { Body = x, RefSender = Self };
                        _commanderActor.Tell(message);
                    }
                });
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
