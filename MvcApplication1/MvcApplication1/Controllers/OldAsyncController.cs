using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class OldAsyncController : AsyncController
    {
        Stopwatch cron;
        public void AcaoAsync(int id = 1)
        {
            cron = new Stopwatch();
            cron.Start();
            AsyncManager.OutstandingOperations.Increment(1);
            var svc = new AsyncOpService.Service1Client();
            svc.GetDataCompleted += (sender, args) => 
            {
                AsyncManager.Parameters["transformedValue"] = args.Result;
                AsyncManager.OutstandingOperations.Decrement();
            };
            svc.GetDataAsync(id);
        }
        public ActionResult AcaoCompleted(string transformedValue)
        {
            var time = Convert.ToDouble(cron.ElapsedMilliseconds) / 1000;
            ViewBag.Time = time;
            return View("RetornoServico", (object)transformedValue);
        }


    }
}
