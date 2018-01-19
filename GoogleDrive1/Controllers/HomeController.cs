using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoogleDrive1.Models;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace GoogleDrive1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string fileId)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var directory = DirectoryModel.Instance;
            directory.GetDirectory(fileId);
            sw.Stop();

            return View(new DebugModel {Files = directory.Files, Quota = QuotaModel.Instance, TimeTaken = sw.ElapsedMilliseconds});
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
