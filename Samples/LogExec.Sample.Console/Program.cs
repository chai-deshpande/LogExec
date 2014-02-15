using System.Diagnostics;
using System.Threading;

namespace LogExec.Sample.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      var executionTimeLogger = new ExecutionTimeLogger("Main function");

      // Enclose the code you are interested in a using block.
      using (var workLogger = new ExecutionTimeLogger("Dummy Work"))
      {
        // Do some useful work.

        // Pretending to do useful work.
        Thread.Sleep(1500);
        workLogger.LogMilestone("DAL completed");

        // Pretending to more useful work.
        Thread.Sleep(400);
        workLogger.LogMilestone("Business logic completed");

        // Useful work done.
        Thread.Sleep(100);
      }
      executionTimeLogger.LogMilestone("Finished dummy work");

      Thread.Sleep(200);

      executionTimeLogger.Stop();
      executionTimeLogger.Log();
    }
  }
}
