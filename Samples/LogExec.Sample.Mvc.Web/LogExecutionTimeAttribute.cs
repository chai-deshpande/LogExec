using System.Web.Mvc;

namespace LogExec.Sample.Mvc.Web
{
  public class LogExecutionTimeAttribute : FilterAttribute, IActionFilter, IResultFilter
  {
    private ExecutionTimeLogger _executionTimeLogger;

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      _executionTimeLogger = new ExecutionTimeLogger(string.Format("{0}.{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName));
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }

    public void OnResultExecuting(ResultExecutingContext filterContext)
    {
    }

    public void OnResultExecuted(ResultExecutedContext filterContext)
    {
      _executionTimeLogger.Stop();
      _executionTimeLogger.Log();
    }
  }
}