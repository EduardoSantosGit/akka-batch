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
            var config = ConfigurationFactory.ParseString(@"
            akka.actor.deployment {
               /workerpool {
                    dispatcher = custom-task-dispatcher
               } 
            }
            custom-task-dispatcher {
              type = TaskDispatcher,
              throughput = 1024
            }
            "); 

            SystemStart = ActorSystem.Create("SystemStart", config);

            var pipeline = new PipeLine(new DataflowOptions
            {
                MonitorInterval = TimeSpan.FromSeconds(2),
                PerformanceMonitorMode = DataflowOptions.PerformanceLogMode.Verbose,
                RecommendedCapacity = 10000,
                RecommendedParallelismIfMultiThreaded = 64,
            }, 20, new System.Net.Http.HttpClient());

            var actor = SystemStart.ActorOf(Props.Create(() =>
                        new ProviderBatchActor(@"C:\Users\staff\Desktop\listacnpjs.txt", pipeline)),
                        ActorPath.Provider.Name);

            actor.Tell(new MessageReader { CountBatch = 1, RefPointer = 0 });

            SystemStart.WhenTerminated.Wait();
        }
    }
}
