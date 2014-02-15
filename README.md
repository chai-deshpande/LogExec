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
    
    // As soon as the ExecutionTimeLogger instance is disposed the execution time 
    // is logged (as shown below) using the logging framework that you are using in your code.

**Sample log file entry**
```
INFO  2014-01-13 20:09:52,918   thread:9  1678ms LogExec.ExecutionTimeLogger => Execution time for [Console Sample]: [1500] ms
```

## Getting Started
The [Getting Started](https://github.com/chai-deshpande/LogExec/wiki/Getting-Started) guide is available on the wiki.

## Reference
The Reference for the ExecutionTimeLogger class can be accessed on the [Reference page](https://github.com/chai-deshpande/LogExec/wiki/Reference) on the wiki.

## Configuring log messages
The log messages can be overridden by adding the following entries in the respective app.config or web.config files of your application (for the message that you intend to change). See the [Customizing Log Messages page](https://github.com/chai-deshpande/LogExec/wiki/Customizing-Log-Messages) on the wiki for more details.

```
  <appSettings>
    <!-- Overridden formats for execution time log messages -->
    <add key="LogExec.Message" value="Custom message exec time for {0}: {1} ms" />
    <add key="LogExec.MilestoneMessage" value="***** MILESTONE ***** Exec time for {0} => {1}]: {2} ms" />
  </appSettings>
```

## Nuget Package
Nuget package is available at https://www.nuget.org/packages/LogExec/
