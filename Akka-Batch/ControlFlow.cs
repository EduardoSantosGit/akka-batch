using Akka.Actor;
using Akka.Batch.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void OnMonitor(string path)
        {
            var lines = SendFileProcess(path);

            foreach (var item in lines)
            {
                this.SendMessagesActor(item);
            }
        }

        public string[] SendFileProcess(string path) => File.ReadAllLines(path);

        public void SendMessagesActor(string line)
        {
            var message = new MessageOneItem { LineData = line };
            _actorRef.Tell(message);
        }


    }
}
