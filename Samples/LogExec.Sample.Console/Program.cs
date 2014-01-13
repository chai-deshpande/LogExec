using System.Threading;

namespace LogExec.Sample.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      using (new ExecutionTimeLogger("Console Sample"))
      {
        Thread.Sleep(1500);
      }
    }
  }
}
