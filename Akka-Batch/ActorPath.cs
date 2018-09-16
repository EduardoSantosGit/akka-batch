using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch
{
    public static class ActorPath
    {

        public const string SystemName = "SystemStart";
        public static readonly ActorMetaData Provider = new ActorMetaData("provider", $"akka://{SystemName}/user/provider");
        public static readonly ActorMetaData Commander = new ActorMetaData("commander", $"akka://{SystemName}/user/commander");
        public static readonly ActorMetaData Coordinator = new ActorMetaData("coordinator", $"akka://{SystemName}/user/coordinator");
        public static readonly ActorMetaData Worker = new ActorMetaData("worker", $"akka://{SystemName}/user/worker");


        public class ActorMetaData
        {
            public ActorMetaData(string name, string path)
            {
                Name = name;
                Path = path;
            }

            public string Name { get; private set; }

            public string Path { get; private set; }
        }
    }
}
