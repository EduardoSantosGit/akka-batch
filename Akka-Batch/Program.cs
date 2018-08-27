using Akka.Actor;
using Akka.Batch.Actors;
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

            var actor = SystemStart.ActorOf(Props.Create(() => new CommanderBatchActor()), ActorPath.Coordinator.Name);

            var control = new ControlFlow(actor);
            control.OnMonitor(@"D:\teste.txt");

            SystemStart.WhenTerminated.Wait();

        }
    }
}
