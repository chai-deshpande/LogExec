using System.Diagnostics;
using System.Threading;

namespace LogExec.Sample.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      var executionTimeLogger = new ExecutionTimeLogger("Main function", true);

      // Enclose the code you are interested in a using block.
      using (var workLogger = new ExecutionTimeLogger("Dummy Work"))
      {
        // Do some useful work.

        // Pretending to do useful work.
        Thread.Sleep(50);
        workLogger.LogMilestone("DAL completed");

        // Pretending to more useful work.
        Thread.Sleep(10);
        workLogger.LogMilestone("Business logic completed");

        // Useful work done.
        Thread.Sleep(10);
      }
      executionTimeLogger.LogMilestone("Finished dummy work");

      Thread.Sleep(40);

      executionTimeLogger.Stop();
      executionTimeLogger.Log();

      // Enclose the code you are interested in a using block including custom thresholds
      using (var workLogger = new ExecutionTimeLogger("Dummy Work with logging types", startImmediately: true, infoOnly: false, warnAbove: 75, errorAbove: 400, fatalAbove: 800))
      {
        // Do some useful work.

        // Pretending to do useful work.
        Thread.Sleep(50);
        workLogger.LogMilestone("DAL completed");

        // Pretending to more useful work.
        Thread.Sleep(400);
        workLogger.LogMilestone("Business logic completed");

        // Pretending to more useful work.
        Thread.Sleep(400);
        workLogger.LogMilestone("External component business logic completed");

        // Useful work done.
        Thread.Sleep(100);
      }
    }
  }
}
