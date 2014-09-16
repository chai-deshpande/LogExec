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
    private readonly bool _infoOnly;
    private readonly long _warnAbove;
    private readonly long _errorAbove;
    private readonly long _fatalAbove;

    // The stopwatch instance.
    private readonly Stopwatch _stopwatch = new Stopwatch();

    /// <summary>
    /// Constructor that accepts an execution context.
    /// </summary>
    /// <param name="executionContext">The execution context is used to print in the log file.</param>
    /// <param name="startImmediately">Starts the timer immediately (default). If false is passed, then you must call the Start method.</param>
    public ExecutionTimeLogger(string executionContext, bool startImmediately = true)
    {
      _executionContext = string.IsNullOrEmpty(executionContext) ? DefaultExecutionContext : executionContext;
      _milestoneLogMessage = ConfigurationManager.AppSettings[MilestoneMessageKey] ?? DefaultMilestoneMessage;
      _logMessage = ConfigurationManager.AppSettings[MessageKey] ?? DefaultLogMessage;

      _infoOnly = true;
      _warnAbove = 0;
      _errorAbove = 0;
      _fatalAbove = 0;

      if (startImmediately)
      {
        _stopwatch.Start();
      }
    }

    /// <summary>
    /// Constructor that accepts an execution context.
    /// </summary>
    /// <param name="executionContext">The execution context is used to print in the log file.</param>
    /// <param name="startImmediately">Starts the timer immediately (default). If false is passed, then you must call the Start method.</param>
    /// <param name="infoOnly">Logs messages only using the Info type</param>
    /// <param name="warnAbove">Logs messages using the Warning type if the elapsed time goes above this threshold</param>
    /// <param name="errorAbove">Logs messages using the Error type if the elapsed time goes above this threshold</param>
    /// <param name="fatalAbove">Logs messages using the Fatal type if the elapsed time goes above this threshold</param>
    public ExecutionTimeLogger(string executionContext, bool startImmediately = true, bool infoOnly = true, long warnAbove = 100, long errorAbove = 500, long fatalAbove = 1000)
    {
      _executionContext = string.IsNullOrEmpty(executionContext) ? DefaultExecutionContext : executionContext;
      _milestoneLogMessage = ConfigurationManager.AppSettings[MilestoneMessageKey] ?? DefaultMilestoneMessage;
      _logMessage = ConfigurationManager.AppSettings[MessageKey] ?? DefaultLogMessage;

      _infoOnly = infoOnly;
      _warnAbove = warnAbove;
      _errorAbove = errorAbove;
      _fatalAbove = fatalAbove;

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
    /// Resumes the paused LogExec timer if it is already paused.
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
      DoLog();
    }

    /// <summary>
    /// Stops the timer and logs the execution time using the configured logger.
    /// </summary>
    public void Dispose()
    {
      _stopwatch.Stop();
      
      DoLog();
    }

    private void DoLog()
    {
      var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
      
      if (_infoOnly || elapsedMilliseconds <= _warnAbove)
      {
        Logger.Info(string.Format(_logMessage, _executionContext, _stopwatch.ElapsedMilliseconds));
      }
      else if (elapsedMilliseconds <= _errorAbove)
      {
        Logger.Warn(string.Format(_logMessage, _executionContext, _stopwatch.ElapsedMilliseconds));
      }
      else if (elapsedMilliseconds <= _fatalAbove)
      {
        Logger.Error(string.Format(_logMessage, _executionContext, _stopwatch.ElapsedMilliseconds));
      }
      else
      {
        Logger.Fatal(string.Format(_logMessage, _executionContext, _stopwatch.ElapsedMilliseconds));
      }
    }
  }
}
