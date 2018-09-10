using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Messages
{
    public class MessageOneItem
    {
        public string LineData { get; set; }
        public IActorRef Sender { get; set; }
    }

    public class MessageSuccess
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public IActorRef Sender { get; set; }
    }
}
