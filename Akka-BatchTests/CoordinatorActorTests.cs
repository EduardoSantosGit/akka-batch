using Akka.Actor;
using Akka.Batch;
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

        }
    }
}
