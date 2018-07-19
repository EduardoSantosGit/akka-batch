using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public class ControlFlow
    {

        private readonly IActorRef _actorRef;

        public ControlFlow(IActorRef actor)
        {
            _actorRef = actor;
        }

        public void SendFileProcess(string path)
        {

        }

    }
}
