using Akka.Batch;
using System;
using Xunit;

namespace Akka_BatchTests
{
    public class ControlFlowTests
    {
        [Fact]
        public void ControlFlow_WhenFileZipCode_ReturnsAllLinesFile()
        {

            var control = new ControlFlow(null);
            var lines = control.SendFileProcess(@"Files\Akka-BatchTests.txt");

            Assert.NotNull(lines);
            Assert.NotEmpty(lines);
            Assert.Equal(8, lines.Length);
        }
    }
}
