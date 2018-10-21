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
            custom-task-dispatcher {
                type = Dispatcher
                throughput = 100
                throughput-deadline-time = 0ms
            }
            akka.actor.deployment {
                /my-actor {
                    dispatcher = custom-task-dispatcher
                }
            }");


            SystemStart = ActorSystem.Create("SystemStart", config);

            var pipeline = new PipeLine(new DataflowOptions
            {
                MonitorInterval = TimeSpan.FromSeconds(2),
                PerformanceMonitorMode = DataflowOptions.PerformanceLogMode.Verbose,
                RecommendedCapacity = 10000,
                RecommendedParallelismIfMultiThreaded = 64,
            }, 20, new System.Net.Http.HttpClient());

            var actor = SystemStart.ActorOf(Props.Create(() => 
                        new ProviderBatchActor(@"C:\Users\eduar\Desktop\lista_cnpj2.txt", pipeline)), 
                        ActorPath.Provider.Name);

            actor.Tell(new MessageReader { CountBatch = 20, RefPointer = 0 });

            SystemStart.WhenTerminated.Wait();
        }
    }
}
