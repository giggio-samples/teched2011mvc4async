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

            var tasks = from e in Enumerable.Range(0, 5) select svc.GetDataAsync(id + e);
            var results = await Task.WhenAll(tasks);
            string transformedValue = string.Join(", ", results);
            
            var time = Convert.ToDouble(cron.ElapsedMilliseconds) / 1000;
            ViewBag.Time = time;
            return View("RetornoServico", (object)transformedValue);
        }
    }
}
