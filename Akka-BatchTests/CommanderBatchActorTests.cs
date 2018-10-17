using Akka.Actor;
using Akka.Batch.Actors;
using Akka.Batch.Messages;
using Akka.TestKit.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Akka_BatchTests
{
    public class CommanderBatchActorTests : TestKit
    {
        [Fact]
        public void CommanderBatchActor_WhenMessageItemsEmpty_NotResponeMessage()
        {
            var actor = Sys.ActorOf(Props.Create(() => new CommanderBatchActor(null)));
            var result = default(MessageReader);

            Within(TimeSpan.FromSeconds(15), () =>
            {
                actor.Tell(new MessageItem { });
                ExpectNoMsg();
            });
        }
    }
}
