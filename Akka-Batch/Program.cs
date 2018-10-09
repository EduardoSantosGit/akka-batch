using Akka.Actor;
using Akka.Batch.Actors;
using Akka.Batch.Messages;
using Akka.Configuration;
using System;

namespace Akka.Batch
{
    class Program
    {
        public static ActorSystem SystemStart;

        static void Main(string[] args)
        {
            SystemStart = ActorSystem.Create("SystemStart");

            var actor = SystemStart.ActorOf(Props.Create(() => 
                        new ProviderBatchActor(@"C:\Users\eduar\Desktop\lista_cnpj.txt")), 
                        ActorPath.Provider.Name);

            actor.Tell(new MessageReader { CountBatch = 10, RefPointer = 0 });

            SystemStart.WhenTerminated.Wait();
        }
    }
}
