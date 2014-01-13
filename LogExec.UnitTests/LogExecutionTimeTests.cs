using System.Threading;
using NUnit.Framework;

namespace LogExec.UnitTests
{
  [TestFixture]
  public class LogExecutionTimeTests
  {
    [Test]
    public void TestPauseAndResume()
    {
      using (var logger = new ExecutionTimeLogger("test"))
      {
        Thread.Sleep(1000);
        Assert.IsTrue(logger.ExecutionTime >= 1000);

        logger.Pause();
        Thread.Sleep(500);
        logger.Resume();

        Assert.IsTrue(logger.ExecutionTime < 1500);
      }
    }
  }
}
