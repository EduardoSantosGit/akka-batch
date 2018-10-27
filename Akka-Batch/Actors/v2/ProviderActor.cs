using Akka.Actor;
using Akka.Batch.Messages.v2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Akka.Batch.Actors.v2
{
    public class ProviderActor : ReceiveActor
    {
        private IActorRef _commanderActor;

        public ProviderActor()
        {
            Starting();
        }

        public void Starting()
        {

            Receive<InitializingReaderMessage>(msg => 
            {
                var file = File.ReadLines(msg.FilePath);
                //if (file.Count() <= 0)
                    

            });



            Become(Reading);
        }

        public void Reading()
        {

        }

        public void Stopping()
        {
        }

        protected override void PreStart()
        {
            _commanderActor = Context.ActorOf(Props.Create(() =>
                new CommanderActor()), ActorPath.Commander.Name);

            base.PreStart();
        }

    }
}
