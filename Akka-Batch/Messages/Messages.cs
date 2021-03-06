﻿using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Messages
{

    public class Message
    {
        public IActorRef RefSender { get; set; }
    }

    public class MessageItem : Message
    {
        public List<string> Batch { get; set; }
        public MessageReader Message { get; set; }
    }

    public class MessageReader : Message
    {
        public int CountBatch { get; set; }
        public int RefPointer { get; set; }
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
