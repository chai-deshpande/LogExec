using System;
using System.Diagnostics;
using System.Reflection;
using Common.Logging;

namespace LogExec
{
  /// <summary>
  /// This class is used to log the execution time for the code that executes within its scope.
  /// </summary>
  public class ExecutionTimeLogger : IDisposable
  {
    private const string DefaultExecutionContext = "UN-NAMED";
    private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    private readonly string _executionContext;
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public ExecutionTimeLogger()
    {
      _executionContext = DefaultExecutionContext;
      _stopwatch.Start();
    }

    /// <summary>
    /// Constructor that accepts an execution context.
    /// </summary>
    /// <param name="executionContext">The execution context is used to print it in the log file.</param>
    public ExecutionTimeLogger(string executionContext)
    {
      _executionContext = string.IsNullOrEmpty(executionContext) ? DefaultExecutionContext : executionContext;
      _stopwatch.Start();
    }

    /// <summary>
    /// Returns the current value of the execution time.
    /// </summary>
    public long ExecutionTime
    {
      get
      {
        return _stopwatch.ElapsedMilliseconds;
      }
    }

    /// <summary>
    /// Pauses the timer used to calculate the execution time.
    /// </summary>
    public void Pause()
    {
      if (_stopwatch.IsRunning)
      {
        _stopwatch.Stop();
      }
    }

    /// <summary>
    /// Resumes the timer paused using the Pause method of LogExec.
    /// </summary>
    public void Resume()
    {
      if (!_stopwatch.IsRunning)
      {
        _stopwatch.Start();
      }
    }

    /// <summary>
    /// Resumes the timer paused using the Pause method of LogExec.
    /// </summary>
    public void Stop()
    {
      if (!_stopwatch.IsRunning)
      {
        _stopwatch.Stop();
      }
    }

    /// <summary>
    /// Logs the effort in milliseconds since the beginning of the execution timer. Includes execution time for all previous milestones (if any).
    /// </summary>
    /// <param name="mileStoneName">The display name of the milestone</param>
    public void LogMilestone(string mileStoneName)
    {
      Logger.Info(string.Format("Execution time for [{0} ===> {2}]: [{1}] ms", _executionContext, _stopwatch.ElapsedMilliseconds, mileStoneName));
    }

    /// <summary>
    /// Logs the effort in milliseconds since the beginning of the execution timer. Includes execution time for all previous milestones (if any).
    /// </summary>
    public void Log()
    {
      Logger.Info(string.Format("Execution time for [{0}]: [{1}] ms", _executionContext, _stopwatch.ElapsedMilliseconds));
    }

    public void Dispose()
    {
      _stopwatch.Stop();
      Logger.Info(string.Format("Execution time for [{0}]: [{1}] ms", _executionContext, _stopwatch.ElapsedMilliseconds));
    }
  }
}
