using Akka.Actor;
using Akka.Batch.Messages;
using Processor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public class WorkerBatchActor : ReceiveActor
    {
        private RequestClient _client;

        public WorkerBatchActor()
        {
            _client = new RequestClient();
            Sending();
        }

        public void Sending()
        {
            Receive<MessageOneItem>(msg => 
            {
                var result = _client.GetDataApi(msg.LineData);

                if(result.Status == ResultCode.OK)
                {
                    Sender.Tell(new MessageSuccess { Status = "OK", Message = "Success", RefSender = msg.RefSender });
                }
                else
                {
                    Sender.Tell(new MessageError { Message = result.Value.ToString() });
                }
            });

        }

    }
}
