using Akka.Actor;
using Akka.Configuration;
using System;

namespace Akka.Batch
{
    class Program
    {
        public static ActorSystem SystemStart;

        static void Main(string[] args)
        {

            var akkaConfig = ConfigurationFactory.ParseString(@"
                akka {  
                    actor {
                        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                    }
                    remote {
                        dot-netty.tcp {
                            port = 8081
                            hostname = 0.0.0.0
                            public-hostname = localhost
                        }
                    }
                }
                ");


            SystemStart = ActorSystem.Create("SystemStart", akkaConfig);

            var actor = SystemStart.ActorOf(Props.Create(() => new CoordinatorBatchActor()), ActorPath.Coordinator.Path);

            var control = new ControlFlow(actor);
            control.OnMonitor("path");

            SystemStart.WhenTerminated.Wait();

        }
    }
}
