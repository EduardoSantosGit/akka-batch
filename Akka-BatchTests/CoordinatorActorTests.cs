using Akka.Actor;
using Akka.Batch;
using Akka.Batch.Messages;
using Akka.TestKit.Xunit2;
using System;
using Xunit;

namespace Akka_BatchTests
{
    public class CoordinatorActorTests : TestKit
    {
        [Fact]
        public void CoordinatorActor_WhenLineString_ReturnsSuccessMessage()
        {

            var actor = Sys.ActorOf(Props.Create(() => new CoordinatorBatchActor()));
            var result = default(MessageSuccess);

            Within(TimeSpan.FromSeconds(15), () => 
            {
                actor.Tell(new MessageOneItem { LineData = "12345676" });
                result = ExpectMsg<MessageSuccess>();
            });

            Assert.Equal("OK", result.Status);
            Assert.Equal("Success", result.Message);
        }
    }
}
