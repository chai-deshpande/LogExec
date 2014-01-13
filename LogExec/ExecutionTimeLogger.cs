using System;
using System.Diagnostics;
using System.Reflection;
using Common.Logging;

namespace LogExec
{
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

    public ExecutionTimeLogger(string executionContext)
    {
      _executionContext = string.IsNullOrEmpty(executionContext) ? DefaultExecutionContext : executionContext;
      _stopwatch.Start();
    }

    public long ExecutionTime
    {
      get
      {
        return _stopwatch.ElapsedMilliseconds;
      }
    }

    public void Pause()
    {
      _stopwatch.Stop();
    }

    public void Resume()
    {
      _stopwatch.Start();
    }

    public void Resstart()
    {
      _stopwatch.Restart();
    }

    public void Reset()
    {
      _stopwatch.Reset();
    }

    /// <summary>
    /// Logs the effort in milliseconds since the beginning of the execution unit. Includes execution time for all previous milestones (if any).
    /// </summary>
    /// <param name="mileStoneName">The display name of the milestone</param>
    public void LogMilestone(string mileStoneName)
    {
      Logger.Info(string.Format("Execution time for [{0} ===> {2}]: [{1}] ms", _executionContext, _stopwatch.ElapsedMilliseconds, mileStoneName));
    }

    public void Dispose()
    {
      _stopwatch.Stop();
      Logger.Info(string.Format("Execution time for [{0}]: [{1}] ms", _executionContext, _stopwatch.ElapsedMilliseconds));
    }
  }
}
