using Akka.Actor;
using Akka.Batch.Actors;
using Akka.Batch.Messages;
using Akka.Configuration;
using Gridsum.DataflowEx;
using Processor;
using System;

namespace Akka.Batch
{
    class Program
    {
        public static ActorSystem SystemStart;

        static void Main(string[] args)
        {
            SystemStart = ActorSystem.Create("SystemStart");

            var pipeline = new PipeLine(new DataflowOptions
            {
                MonitorInterval = TimeSpan.FromSeconds(2),
                PerformanceMonitorMode = DataflowOptions.PerformanceLogMode.Verbose,
                //FlowMonitorEnabled = false,
                //BlockMonitorEnabled = false,
                RecommendedCapacity = 10000,
                RecommendedParallelismIfMultiThreaded = 64,
            }, 10);

            var actor = SystemStart.ActorOf(Props.Create(() => 
                        new ProviderBatchActor(@"C:\Users\eduar\Desktop\lista_cnpj.txt", pipeline)), 
                        ActorPath.Provider.Name);

            actor.Tell(new MessageReader { CountBatch = 10, RefPointer = 0 });

            SystemStart.WhenTerminated.Wait();
        }
    }
}
