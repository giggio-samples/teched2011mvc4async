using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class NotAsyncController : Controller
    {
        public ActionResult Acao(int id = 3)
        {
            var cron = new Stopwatch();
            cron.Start();
            
            var svc = new TaskService.Service1Client();
            string transformedValue = "";
            for (int i = 0; i < 5; i++)
            {
                transformedValue += svc.GetData(id) + ", ";
                id++;
            }
            var time = Convert.ToDouble(cron.ElapsedMilliseconds) / 1000;
            
            ViewBag.Time = time;
            return View("RetornoServico", (object)transformedValue);
        }
    }
}
