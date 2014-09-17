LogExec
=======

Logs the execution time for units/blocks of code. Often times while optimizing code for performance we need to know what is the execution time for blocks of code. LogExec can be very easily used to log the execution time for blocks of code. Using a library like LogExec helps avoid a lot of the boilerplate code.

LogExec uses the Common.Logging abstraction library so that it is not tied to any specific logging provider.

Following is a sample usage of LogExec:
```
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
```

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

## Releases
**16-Sep-2014**
  1. Fixed a backward compatibility problem
  
**8-Aug-2014**
  1. Added ability to specify thresholds for warning, error and fatal log messages 
  2. This adds the ability to do various things like e.g. add an event viewer entry if the execution time goes above a certain level and send emails if it is in excess of yet another level
  3. Details are [here](./Thresholds-for-warning,-error-and-fatal-messages)

**15-Feb-2014**
  1. Added ability to override log messages
  2. The constructor now has a parameter to not start the timer immediately with an additional Start method.

**18-Jan-2014**
  1. LogExec for logging execution time 
  2. Sample console application 
  3. Sample MVC web application that has a filter that logs the execution time for any web request (action method)
