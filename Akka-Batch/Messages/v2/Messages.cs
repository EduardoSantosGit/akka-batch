using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Batch.Messages.v2
{
    public class InitializingReaderMessage
    {
        public string FilePath { get; set; }

        public InitializingReaderMessage(string path)
        {
            FilePath = path;
        }
    }

    public class StartReaderMessage
    {
        public int BatchCount { get; set; }
    }
}
