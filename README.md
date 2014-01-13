LogExec
=======

Logs the execution time for units of code. Following is a sample usage:

    // Enclose the code you are interested in a using block.
    using (new ExecutionTimeLogger("Console Sample"))
    {
      // Do some useful work.
  
      // Pretending to do useful work.
      Thread.Sleep(1500);
  
      // Useful work done.
    }

Sample log file entry
=====================
INFO  2014-01-13 20:09:52,918   thread:9  1678ms LogExec.ExecutionTimeLogger => Execution time for [Console Sample]: [1500] ms
