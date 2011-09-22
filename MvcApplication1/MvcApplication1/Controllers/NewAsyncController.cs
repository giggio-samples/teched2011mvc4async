﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class NewAsyncController : AsyncController
    {
        public async Task<ActionResult> Acao(int id = 2)
        {
            var cron = new Stopwatch();
            cron.Start();
            var svc = new TaskService.Service1Client();
            var transformedValue = await svc.GetDataAsync(id);
            var time = Convert.ToDouble(cron.ElapsedMilliseconds) / 1000;
            ViewBag.Time = time;
            return View("RetornoServico", (object)transformedValue);
        }
    }
}