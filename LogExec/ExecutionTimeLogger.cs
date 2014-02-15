using System;
using System.Configuration;
using System.Diagnostics;
using Common.Logging;

namespace LogExec
{
  /// <summary>
  /// This class is used to log the execution time for the code that executes within its scope.
  /// </summary>
  public class ExecutionTimeLogger : IDisposable
  {
    // Default messages.
    private const string DefaultExecutionContext = "UN-NAMED";
    private const string DefaultLogMessage = "Execution time for [{0}]: [{1}] ms";
    private const string DefaultMilestoneMessage = "Execution time for [{0} ===> {1}]: [{2}] ms";
    
    // Configuration keys.
    private const string MessageKey = "LogExec.Message";
    private const string MilestoneMessageKey = "LogExec.MilestoneMessage";
    
    // Logged instance.
    private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

    // Member variables.
    private readonly string _executionContext;
    private readonly string _milestoneLogMessage;
    private readonly string _logMessage;

    // The stopwatch instance.
    private readonly Stopwatch _stopwatch = new Stopwatch();

    /// <summary>
    /// Constructor that accepts an execution context.
    /// </summary>
    /// <param name="executionContext">The execution context is used to print it in the log file.</param>
    /// <param name="startImmediately">Starts the timer immediately (default). If false is passed, then you must call the Start method.</param>
    public ExecutionTimeLogger(string executionContext, bool startImmediately = true)
    {
      _executionContext = string.IsNullOrEmpty(executionContext) ? DefaultExecutionContext : executionContext;
      _milestoneLogMessage = ConfigurationManager.AppSettings[MilestoneMessageKey] ?? DefaultMilestoneMessage;
      _logMessage = ConfigurationManager.AppSettings[MessageKey] ?? DefaultLogMessage;

      if (startImmediately)
      {
        _stopwatch.Start();
      }
    }

    /// <summary>
    /// Returns the current execution time.
    /// </summary>
    public long ExecutionTime
    {
      get
      {
        return _stopwatch.ElapsedMilliseconds;
      }
    }

    /// <summary>
    /// Pauses the LogExec timer used to calculate the execution time.
    /// </summary>
    public void Pause()
    {
      if (_stopwatch.IsRunning)
      {
        _stopwatch.Stop();
      }
    }

    /// <summary>
    /// Resumes the paused LogExec timer.
    /// </summary>
    public void Resume()
    {
      if (!_stopwatch.IsRunning)
      {
        _stopwatch.Start();
      }
    }

    /// <summary>
    /// Starts the LogExec timer if it is not already running.
    /// </summary>
    public void Start()
    {
      if (!_stopwatch.IsRunning)
      {
        _stopwatch.Start();
      }
    }

    /// <summary>
    /// Stops the LogExec timer if it is running.
    /// </summary>
    public void Stop()
    {
      if (_stopwatch.IsRunning)
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
      Logger.Info(string.Format(_milestoneLogMessage, _executionContext, mileStoneName, _stopwatch.ElapsedMilliseconds));
    }

    /// <summary>
    /// Logs the effort in milliseconds since the beginning of the execution timer. Includes execution time for all previous milestones (if any).
    /// </summary>
    public void Log()
    {
      Logger.Info(string.Format(_logMessage, _executionContext, _stopwatch.ElapsedMilliseconds));
    }

    /// <summary>
    /// Stops the timer and logs the execution time using the configured logger.
    /// </summary>
    public void Dispose()
    {
      _stopwatch.Stop();
      Logger.Info(string.Format(_logMessage, _executionContext, _stopwatch.ElapsedMilliseconds));
    }
  }
}
