using System.Threading;

namespace LogExec.Sample.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      // Enclose the code you are interested in a using block.
      using (new ExecutionTimeLogger("Console Sample"))
      {
        // Do some useful work.

        // Pretending to do useful work.
        Thread.Sleep(1500);

        // Useful work done.
      }
    }
  }
}
