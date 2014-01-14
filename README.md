LogExec
=======

Logs the execution time for units/blocks of code. Often times while optimizing code for performance we need to know what is the execution time for blocks of code. LogExec can be very easily used to print the execution time for blocks of code.

LogExec uses the Common.Logging abstraction library so that it is not tied to any specific logging provider.

Following is a sample usage of LogExec:

    // Enclose the code you want to find the execution time for in a using block.
    using (new ExecutionTimeLogger("Console Sample"))
    {
      // Do some useful work.
  
      // Pretending to do useful work.
      Thread.Sleep(1500);
  
      // Useful work done.
    }
    
    // As soon as the ExecutionTimeLogger instance is disposed the execution time is logged (as shown below) using the logging framework that you are using in your code.

Sample log file entry
=====================
INFO  2014-01-13 20:09:52,918   thread:9  1678ms LogExec.ExecutionTimeLogger => Execution time for [Console Sample]: [1500] ms
