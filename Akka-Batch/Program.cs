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
            SystemStart = ActorSystem.Create("SystemStart");

            var actor = SystemStart.ActorOf(Props.Create(() => new CoordinatorBatchActor()), ActorPath.Coordinator.Name);

            var control = new ControlFlow(actor);
            control.OnMonitor(@"C:\Users\staff\Desktop\data.txt");

            SystemStart.WhenTerminated.Wait();

        }
    }
}
