using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace MvcApplication1.Controllers
{
    public class NewAsyncController : AsyncController
    {
        public async Task<ActionResult> Acao(int id = 2)
        {
            var cron = new Stopwatch();
            cron.Start();
            var svc = new TaskService.Service1Client();
            string transformedValue = "";
            var tasks = new List<Task<string>>();
            for (int i = 0; i < 5; i++)
            {
                var task = svc.GetDataAsync(id);
                tasks.Add(task);
                id++;
            }
            Task.WhenAll(tasks).ContinueWith(taskResults => transformedValue = string.Join(", ", taskResults.Result));
            Task.WaitAll(tasks.ToArray());
            var time = Convert.ToDouble(cron.ElapsedMilliseconds) / 1000;
            ViewBag.Time = time;
            return View("RetornoServico", (object)transformedValue);
        }
    }
}
