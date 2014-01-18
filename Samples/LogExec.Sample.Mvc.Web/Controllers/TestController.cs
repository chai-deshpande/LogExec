using System.Threading;
using System.Web.Mvc;

namespace LogExec.Sample.Mvc.Web.Controllers
{
  [LogExecutionTime]
  public class TestController : Controller
  {
    //
    // GET: /Test/

    public ActionResult Index()
    {
      // Do some work.
      Thread.Sleep(450);

      return View();
    }

  }
}
