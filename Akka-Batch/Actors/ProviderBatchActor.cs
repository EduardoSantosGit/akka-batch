using Akka.Actor;
using Akka.Batch.Messages;
using Processor;
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
        private PipeLine _pipeline;
        private int _countLines;

        public ProviderBatchActor(string path, PipeLine pipeline)
        {
            _path = path;
            _countLines = File.ReadLines(_path).Count();
            _pipeline = pipeline;

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
                
                Console.WriteLine("Actor {0}, count {1} , lines {2}", 
                    Self.Path, msg.CountBatch, lines.Count);

                _commanderActor.Tell(message);
            });
        }

        protected override void PreStart()
        {
            _commanderActor = Context.ActorOf(Props.Create(() =>
                new CommanderBatchActor(_pipeline)), ActorPath.Commander.Name);

            base.PreStart();
        }

    }
}
