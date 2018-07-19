using Akka.Actor;
using Akka.Configuration;
using System;

namespace Akka_Batch
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
            //var messages = SystemStart.ActorOf(Props.Create(() => new ReceiverMessageActor()), ActorPath.Receiver.Name);
            //messages.Tell("start");
            SystemStart.WhenTerminated.Wait();

        }
    }
}
