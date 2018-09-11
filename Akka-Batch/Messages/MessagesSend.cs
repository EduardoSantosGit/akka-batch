using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Messages
{
    public class Message
    {
        public IActorRef Sender { get; set; }
    }

    public class MessageOneItem : Message
    {
        public string LineData { get; set; }
    }

    public class MessageSuccess : Message
    {
        public string Message { get; set; }
        public string Status { get; set; }
    }

    public class MessageError : MessageSuccess { }
}
