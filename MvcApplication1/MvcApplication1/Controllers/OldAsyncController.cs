using System;
using System.Collections.Generic;
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
            AsyncManager.OutstandingOperations.Increment(5);
            var svc = new AsyncOpService.Service1Client();
            AsyncManager.Parameters["transformedValue"] = "";
            svc.GetDataCompleted += (sender, args) =>
            {
                AsyncManager.Parameters["transformedValue"] = ((string)AsyncManager.Parameters["transformedValue"]) + args.Result + ", ";
                AsyncManager.OutstandingOperations.Decrement();
            };
            for (int i = 0; i < 5; i++)
            {
                svc.GetDataAsync(id);
                id++;
            }
        }
        public ActionResult AcaoCompleted(string transformedValue)
        {

            var time = Convert.ToDouble(cron.ElapsedMilliseconds) / 1000;
            ViewBag.Time = time;
            return View("RetornoServico", (object)transformedValue);
        }


    }
}
